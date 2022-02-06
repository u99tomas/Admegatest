using Application.Interfaces.Repositories;
using Application.Mappings;
using Application.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetAllPaged
{
    public class GetAllPagedUsersQuery : IRequest<PagedResponse<GetAllPagedUsersResponse>>
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

    internal class GetAllPagedUsersHandler : IRequestHandler<GetAllPagedUsersQuery, PagedResponse<GetAllPagedUsersResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedUsersHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<GetAllPagedUsersResponse>> Handle(GetAllPagedUsersQuery query, CancellationToken cancellationToken)
        {
            var users = _unitOfWork.Repository<User>().Entities;

            if (!string.IsNullOrEmpty(query.SearchString))
            {
                users = users.Where(u => u.Name.Contains(query.SearchString));
            }

            switch (query.SortLabel)
            {
                case "Name":

                    if (query.SortDirection == "Descending")
                        users = users.OrderByDescending(u => u.Name);
                    else
                        users = users.OrderBy(u => u.Name);
                    break;
            }

            return await users.Select(r =>
                new GetAllPagedUsersResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToPagedResponseAsync(query.Page, query.PageSize);
        }
    }
}
