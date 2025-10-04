using AutoMapper;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Contracts.WriteRepositories;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Services;

namespace TravelBot.Services.Services
{
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService, IServiceAnchor
    {
        private readonly IUserReadRepository userReadRepository;
        private readonly IUserWriteRepository userWriteRepository;
        private readonly IPassportReadRepository passportReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public UserService(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IPassportReadRepository passportReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userReadRepository = userReadRepository;
            this.userWriteRepository = userWriteRepository;
            this.passportReadRepository = passportReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        async Task<UserModel> IUserService.Create(UserCreateModel model, CancellationToken cancellationToken)
        {
            var item = mapper.Map<User>(model);

            userWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserModel>(item);
        }

        async Task IUserService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var item = await userReadRepository.GetByIdRaw(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");

            userWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<UserModel> IUserService.Edit(Guid id, UserCreateModel model, CancellationToken cancellationToken)
        {
            var item = await userReadRepository.GetByIdRaw(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");
            var passport = await passportReadRepository.GetByIdRaw(model.PassportId, cancellationToken)
                           ?? throw new TravelBotNotFoundException($"Не удалось найти паспорт с идентификатором {model.PassportId}");

            mapper.Map(model, item);

            userWriteRepository.Update(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            item.Passport = passport;
            var result = mapper.Map<UserModel>(item);

            return result;
        }

        async Task<IReadOnlyCollection<UserModel>> IUserService.GetAll(CancellationToken cancellationToken)
        {
            var items = await userReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<UserModel>>(items);
        }

        async Task<UserModel> IUserService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await userReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");

            return mapper.Map<UserModel>(item);
        }
    }
}
