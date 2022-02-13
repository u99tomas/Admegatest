using Application.Interfaces.Repositories;
using Application.Mappings;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetAllPaged
{
    public class GetAllPagedUsersQuery : IRequest<PagedResult<GetAllPagedUsersResponse>>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        /// <summary>
        /// "Ascending" ordena de forma ascendente <br></br>
        /// "Descending" ordena de forma descendente
        /// </summary>
        public string SortDirection { get; set; }
        public string SortLabel { get; set; }
        public string SearchString { get; set; }
    }

    internal class GetAllPagedUsersHandler : IRequestHandler<GetAllPagedUsersQuery, PagedResult<GetAllPagedUsersResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedUsersHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<GetAllPagedUsersResponse>> Handle(GetAllPagedUsersQuery query, CancellationToken cancellationToken)
        {
            var users = _unitOfWork.Repository<User>().Entities;

            if (!string.IsNullOrEmpty(query.SearchString))
            {
                users = users.Where(u => u.AccountName.Contains(query.SearchString));
            }

            switch (query.SortLabel)
            {
                case "AccountName":
                    users = users.SortBy(u => u.AccountName, query.SortDirection);
                    break;
            }

            return await users.Select(r =>
                new GetAllPagedUsersResponse
                {
                    Id = r.Id,
                    AccountName = r.AccountName,
                }).ToPagedResponseAsync(query.Page, query.PageSize);
        }
    }
}
