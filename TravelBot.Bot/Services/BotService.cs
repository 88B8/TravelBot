using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TravelBot.Common;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Contracts.WriteRepositories;

namespace TravelBot.Bot.Services
{
    public class BotService : BackgroundService
    {
        private readonly ITelegramBotClient botClient;
        private readonly IUserReadRepository userReadRepository;
        private readonly IRouteReadRepository routeReadRepository;
        private readonly IPlaceReadRepository placeReadRepository;
        private readonly IUserWriteRepository userWriteRepository;
        private readonly IPassportWriteRepository passportWriteRepository;
        private readonly IPassportPlaceWriteRepository passportPlaceWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ConcurrentDictionary<long, bool> isWaitingForName = new();

        /// <summary>
        /// ctor
        /// </summary>
        public BotService(ITelegramBotClient botClient, IUserReadRepository userReadRepository, IRouteReadRepository routeReadRepository, IPlaceReadRepository placeReadRepository, IUserWriteRepository userWriteRepository, IPassportWriteRepository passportWriteRepository, IPassportPlaceWriteRepository passportPlaceWriteRepository, IUnitOfWork unitOfWork)
        {
            this.botClient = botClient;
            this.userReadRepository = userReadRepository;
            this.routeReadRepository = routeReadRepository;
            this.placeReadRepository = placeReadRepository;
            this.userWriteRepository = userWriteRepository;
            this.passportWriteRepository = passportWriteRepository;
            this.passportPlaceWriteRepository = passportPlaceWriteRepository;
            this.unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            botClient.StartReceiving(
                updateHandler: UpdateHandler,
                errorHandler: ErrorHandler,
                cancellationToken: cancellationToken);
        }

        private async Task UpdateHandler(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message || update.Message?.Text == null)
                return;

            var userId = update.Message.From!.Id;
            var text = update.Message.Text.Trim();

            if (text.StartsWith("/start"))
            {
                var parts = text.Split(' ', 2);
                var parameter = parts.Length > 1 ? parts[1] : null;

                if (!await IsUserRegistered(userId, cancellationToken))
                {
                    await botClient.SendMessage(userId, "Привет! Как тебя зовут?", cancellationToken: cancellationToken);
                    isWaitingForName.TryAdd(userId, true);
                }
                else if (!string.IsNullOrEmpty(parameter) && Guid.TryParse(parameter, out var placeId))
                {
                    await AddPlaceFromParameter(userId, placeId, botClient, cancellationToken);

                    await SendMainKeyboard(userId, botClient, cancellationToken);
                }
                else
                {
                    await botClient.SendMessage(userId, "С возвращением!", cancellationToken: cancellationToken);
                    await SendMainKeyboard(userId, botClient, cancellationToken);
                }

                return;
            }

            if (isWaitingForName.TryGetValue(userId, out var waiting) && waiting)
            {
                await ProcessNameInput(userId, text, cancellationToken);
                isWaitingForName.TryRemove(userId, out _);

                await botClient.SendMessage(
                    chatId: userId,
                    text: $"Рад познакомиться, {text}! 👋 Теперь ты зарегистрирован.",
                    cancellationToken: cancellationToken);

                await SendMainKeyboard(userId, botClient, cancellationToken);
                return;
            }

            switch (text)
            {
                case "Показать маршруты":
                    await ShowRoutes(userId, botClient, cancellationToken);
                    break;
                case "Мой паспорт":
                    await ShowPassport(userId, botClient, cancellationToken);
                    break;
                case "Помощь":
                    await botClient.SendMessage(userId, "Справка по боту...", cancellationToken: cancellationToken);
                    break;
                case "Контакты":
                    await botClient.SendMessage(userId, "Наши контакты: ...", cancellationToken: cancellationToken);
                    break;
                default:
                    await botClient.SendMessage(userId, "Неизвестная команда. Выберите кнопку.", cancellationToken: cancellationToken);
                    break;
            }
        }

        private async Task ProcessNameInput(long userId, string name, CancellationToken cancellationToken)
        {
            if (await IsUserRegistered(userId, cancellationToken))
            {
                return;
            }

            var newPassport = new Entities.Passport();

            var newUser = new Entities.User
            {
                Name = name,
                TelegramId = userId,
                Passport = newPassport,
            };

            passportWriteRepository.Add(newPassport);
            userWriteRepository.Add(newUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ShowRoutes(long userId, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            if (!await IsUserRegistered(userId, cancellationToken))
            {
                await client.SendMessage(userId, "У вас нет доступа. Используйте /start", cancellationToken: cancellationToken);
                return;
            }

            var routes = await routeReadRepository.GetAll(cancellationToken);

            if (!routes.Any())
            {
                await client.SendMessage(userId, "Маршрутов пока нет.", cancellationToken: cancellationToken);
                return;
            }

            foreach (var route in routes)
            {
                var message = "🌟 *Маршрут*\n\n" +
                              $"📍 *Отправная точка:* {route.StartPoint}\n" +
                              $"🕒 *Среднее время:* {route.AverageTime} мин\n" +
                              $"💰 *Бюджет:* {route.Budget}\n" +
                              $"🌤 *Сезон:* {route.Season.GetDisplayName()}\n" +
                              $"✨ *Причина посетить:* {route.ReasonToVisit}\n\n" +
                              "🏞 *Места:*\n";

                if (route.Places.Any())
                {
                    foreach (var place in route.Places)
                    {
                        message += $"• {place.Name}\n";
                    }
                }
                else
                {
                    message += "Места не указаны\n";
                }

                await client.SendMessage(
                    chatId: userId,
                    text: message,
                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken
                );
            }
        }

        private async Task ShowPassport(long userId, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            if (!await IsUserRegistered(userId, cancellationToken))
            {
                await client.SendMessage(userId, "У вас нет доступа. Используйте /start", cancellationToken: cancellationToken);
                return;
            }

            var message = "📄Ваш туристический паспорт: \n\n";

            var user = await userReadRepository.GetByTelegramId(userId, cancellationToken);

            if (user == null)
            {
                Console.WriteLine($"Пользователь с идентификатором не найден {userId}");
                return;
            }

            message += $"🏷️Ваше имя: {user.Name}\n" +
                       $"🏞Посещенные места:\n";

            if (user.Passport.Places.Any())
            {
                foreach (var place in user.Passport.Places)
                {
                    message += $"{place.Name}\n";
                }
            }
            else
            {
                message += "Нет посещенных мест.";
            }

            await botClient.SendMessage(userId, message, cancellationToken: cancellationToken);
        }

        private async Task AddPlaceFromParameter(long userId, Guid id, ITelegramBotClient client, CancellationToken cancellationToken)
        {
            if (!await IsUserRegistered(userId, cancellationToken))
            {
                return;
            }

            var place = await placeReadRepository.GetById(id, cancellationToken);
            var user = await userReadRepository.GetByTelegramId(userId, cancellationToken);

            if (user == null)
            {
                Console.WriteLine($"Пользователь с идентификатором не найден {userId}");
                return;
            }

            if (place == null)
            {
                await client.SendMessage(userId, "QR код недействителен.", cancellationToken: cancellationToken);
                return;
            }

            if (user.Passport.Places.Any(p => p.Id == id))
            {
                await client.SendMessage(userId, $"Место {place.Name} уже добавлено в ваш туристический паспорт!", cancellationToken: cancellationToken);
                return;
            }

            var passportPlace = new PassportPlace
            {
                PlaceId = id,
                PassportId = user.Passport.Id,
            };

            passportPlaceWriteRepository.Add(passportPlace);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await client.SendMessage(userId, $"Место {place.Name} добавлено в ваш туристический паспорт!", cancellationToken: cancellationToken);
        }

        private async Task SendMainKeyboard(long userId, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "Показать маршруты", "Мой паспорт" },
                new KeyboardButton[] { "Помощь", "Контакты" }
            })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = false,
            };

            await botClient.SendMessage(userId, "Выберите действие", replyMarkup: replyKeyboard, cancellationToken: cancellationToken);
        }

        private async Task ErrorHandler(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
        {
            Console.WriteLine($"Error: {exception.Message}");
            await Task.CompletedTask;
        }

        private async Task<bool> IsUserRegistered(long userId, CancellationToken cancellationToken)
            => await userReadRepository.GetByTelegramId(userId, cancellationToken) != null;
    }
}
