using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IRepositoryAsync<Permission, int> _repository;

        public PermissionRepository(IRepositoryAsync<Permission, int> repository)
        {
            _repository = repository;
        }
    }
}
