using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IRepositoryAsync<Role, int> _repository;

        public RoleRepository(IRepositoryAsync<Role, int> repository)
        {
            _repository = repository;
        }

        public bool AnyWIthName(string name)
        {
            return _repository.Entities.Any(x => x.Name == name);
        }
    }
}
