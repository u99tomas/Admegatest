using Application.Enums;
using Application.Extensions;
using Application.Interfaces.Repositories;
using Application.Mappings;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Companies.Queries.GetAllPaged
{
    public class GetAllPagedCompaniesQuery : IRequest<PagedResult<GetAllPagedCompaniesResponse>>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public SortDirection SortDirection { get; set; }
        public string SortLabel { get; set; }
        public string SearchString { get; set; }
    }

    internal class GetAllPagedCompaniesQueryHandler : IRequestHandler<GetAllPagedCompaniesQuery, PagedResult<GetAllPagedCompaniesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedCompaniesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<GetAllPagedCompaniesResponse>> Handle(GetAllPagedCompaniesQuery query, CancellationToken cancellationToken)
        {
            var companies = _unitOfWork.Repository<Company>().Entities;

            companies = companies.Filter(c => c.CompanyName.Contains(query.SearchString) || c.Denomination.Contains(query.SearchString), query.SearchString);

            switch (query.SortLabel)
            {
                case "CompanyName":
                    companies = companies.OrderBy(c => c.CompanyName, query.SortDirection);
                    break;

                case "Denomination":
                    companies = companies.OrderBy(c => c.Denomination, query.SortDirection);
                    break;

                default:
                    companies = companies.OrderBy(c => c.CompanyName, SortDirection.Ascending);
                    break;
            }

            return await companies.Select(c => new GetAllPagedCompaniesResponse
            {
                Id = c.Id,
                CompanyName = c.CompanyName,
                Denomination = c.Denomination,
            }).ToPagedResultAsync(query.Page, query.PageSize);
        }
    }
}
