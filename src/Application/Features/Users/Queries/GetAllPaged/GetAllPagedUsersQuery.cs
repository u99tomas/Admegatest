using Application.Enums;
using Application.Extensions;
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
        public SortDirection SortDirection { get; set; }
        public string SortLabel { get; set; }
        public string SearchString { get; set; }
    }

    internal class GetAllPagedUsersQueryHandler : IRequestHandler<GetAllPagedUsersQuery, PagedResult<GetAllPagedUsersResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedUsersQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<GetAllPagedUsersResponse>> Handle(GetAllPagedUsersQuery query, CancellationToken cancellationToken)
        {
            var users = _unitOfWork.Repository<User>().Entities;

            users = users.Filter(u => u.Name.Contains(query.SearchString), query.SearchString);

            switch (query.SortLabel)
            {
                case "Name":
                    users = users.OrderBy(u => u.Name, query.SortDirection);
                    break;

                case "Enabled":
                    users = users.OrderBy(u => u.Enabled, query.SortDirection);
                    break;

                default:
                    users = users.OrderBy(u => u.Name, SortDirection.Ascending);
                    break;
            }

            return await users.Select(u =>
                new GetAllPagedUsersResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Password = u.Password,
                    Enabled = u.Enabled,
                }).ToPagedResultAsync(query.Page, query.PageSize);
        }
    }
}
