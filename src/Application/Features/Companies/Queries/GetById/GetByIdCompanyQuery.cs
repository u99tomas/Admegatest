using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Companies.Queries.GetById
{
    public class GetByIdCompanyQuery : IRequest<Result<GetByIdCompanyResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetByIdCompanyQueryHandler : IRequestHandler<GetByIdCompanyQuery, Result<GetByIdCompanyResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetByIdCompanyQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetByIdCompanyResponse>> Handle(GetByIdCompanyQuery query, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Company>().Entities.Where(c => c.Id == query.Id)
                .Select(c => new GetByIdCompanyResponse
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    Denomination = c.Denomination
                })
                .FirstOrDefaultAsync();

            if (company == null)
            {
                return Result<GetByIdCompanyResponse>.Failure("No se ha encontrado la empresa");
            }

            return Result<GetByIdCompanyResponse>.Success(company);
        }
    }
}
