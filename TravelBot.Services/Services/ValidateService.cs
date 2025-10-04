using FluentValidation;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Services;
using TravelBot.Services.Validators;

namespace TravelBot.Services.Services
{
    /// <inheritdoc cref="IValidateService"/>
    public class ValidateService : IValidateService, IServiceAnchor
    {
        private readonly Dictionary<Type, IValidator> validators;

        /// <summary>
        /// ctor
        /// </summary>
        public ValidateService()
        {
            validators = new Dictionary<Type, IValidator>()
            {
                { typeof(AdminCreateModel), new AdminCreateModelValidator() },
                { typeof(CategoryCreateModel), new CategoryCreateModelValidator() },
                { typeof(PassportCreateModel), new PassportCreateModelValidator() },
                { typeof(PlaceCreateModel), new PlaceCreateModelValidator() },
                { typeof(RoutePlaceCreateModel), new RoutePlaceCreateModelValidator() },
                { typeof(RouteCreateModel), new RouteCreateModelValidator() },
                { typeof(UserCreateModel), new UserCreateModelValidator() },
                { typeof(PassportPlaceCreateModel), new PassportPlaceCreateModelValidator() },
            };
        }

        async Task IValidateService.Validate<TModel>(TModel model, CancellationToken cancellationToken)
        {
            if (!validators.TryGetValue(typeof(TModel), out var validatorObj))
            {
                throw new TravelBotInvalidOperationException($"Валидатор для типа {typeof(TModel).Name} не найден");
            }

            if (validatorObj is not IValidator<TModel> validator)
            {
                throw new TravelBotInvalidOperationException($"Неверный тип валидатора для {typeof(TModel).Name}");
            }

            var result = await validator.ValidateAsync(model, cancellationToken);

            if (!result.IsValid)
            {
                throw new TravelBotValidationException(result.Errors.Select(x =>
                    InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)
                ));
            }
        }
    }
}
