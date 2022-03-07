using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryAsync<User, int> _repository;

        public UserRepository(IRepositoryAsync<User, int> repository)
        {
            _repository = repository;
        }

        public bool AnyWIthName(string name)
        {
            return _repository.Entities.Any(x => x.Name == name);
        }
    }
}
