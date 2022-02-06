using Application.Interfaces.Repositories;
using Application.Mappings;
using Application.Wrapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Queries.GetAllPaged
{
    public class GetAllPagedRolesQuery : IRequest<PagedResponse<GetAllPagedRolesResponse>>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        /// <summary>
        /// "ascending" ordena de forma ascendente <br></br>
        /// "descending" ordena de forma descendente
        /// </summary>
        public string SortDirection { get; set; }
        public string SortLabel { get; set; }
        public string SearchString { get; set; }
    }

    internal class GetAllPagedRolesHandler : IRequestHandler<GetAllPagedRolesQuery, PagedResponse<GetAllPagedRolesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedRolesHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<GetAllPagedRolesResponse>> Handle(GetAllPagedRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _unitOfWork.Repository<Role>().Entities;

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                roles = roles.Where(
                    r => r.Name.Contains(request.SearchString) ||
                    r.Description.Contains(request.SearchString)
                );
            }

            switch (request.SortLabel)
            {
                case "Name":

                    if (request.SortDirection == "descending")
                        roles = roles.OrderByDescending(r => r.Name);
                    else
                        roles = roles.OrderBy(r => r.Name);
                    break;

                case "Description":

                    if (request.SortDirection == "descending")
                        roles = roles.OrderByDescending(r => r.Description);
                    else
                        roles = roles.OrderBy(r => r.Description);
                    break;
            }

            return await roles.Select(r =>
                new GetAllPagedRolesResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                }).ToPagedResponseAsync(request.Page, request.PageSize);
        }
    }
}
