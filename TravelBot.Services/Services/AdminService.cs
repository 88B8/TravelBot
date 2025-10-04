using AutoMapper;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Contracts.WriteRepositories;
using TravelBot.Services.Contracts.Auth;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Services;

namespace TravelBot.Services.Services
{
    /// <inheritdoc cref="IAdminService"/>
    public class AdminService : IAdminService, IServiceAnchor
    {
        private readonly IAdminReadRepository adminReadRepository;
        private readonly IAdminWriteRepository adminWriteRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public AdminService(IAdminReadRepository adminReadRepository, IAdminWriteRepository adminWriteRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.adminReadRepository = adminReadRepository;
            this.adminWriteRepository = adminWriteRepository;
            this.passwordHasher = passwordHasher;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        async Task<AdminModel> IAdminService.Create(AdminCreateModel model, CancellationToken cancellationToken)
        {
            var item = new Admin
            {
                Login = model.Login,
                Name = model.Name,
                PasswordHash = passwordHasher.Generate(model.Password),
            };

            adminWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<AdminModel>(item);
        }

        async Task IAdminService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var item = await adminReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти администратора с идентификатором {id}");

            adminWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<AdminModel> IAdminService.Edit(Guid id, AdminCreateModel model, CancellationToken cancellationToken)
        {
            var item = await adminReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти администратора с идентификатором {id}");

            mapper.Map(model, item);

            adminWriteRepository.Update(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedEntity = await adminReadRepository.GetById(id, cancellationToken);

            return mapper.Map<AdminModel>(updatedEntity);
        }

        async Task<IReadOnlyCollection<AdminModel>> IAdminService.GetAll(CancellationToken cancellationToken)
        {
            var items = await adminReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<AdminModel>>(items);
        }

        async Task<AdminModel> IAdminService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await adminReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти администратора с идентификатором {id}");

            return mapper.Map<AdminModel>(item);
        }
    }
}
