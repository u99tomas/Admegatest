using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IRepositoryAsync<Company, int> _repository;

        public CompanyRepository(IRepositoryAsync<Company, int> repository)
        {
            _repository = repository;
        }
    }
}
