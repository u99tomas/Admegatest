using Application.Extensions;
using Application.Interfaces.Repositories;
using Application.Mappings;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Permissions.Queries.GetAllPaged
{
    public class GetAllPagedPermissionsQuery : IRequest<PagedResult<GetAllPagedPermissionsResponse>>
    {
        public int RoleId { get; set; }
        public int GroupId { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string SortDirection { get; set; }
        public string SortLabel { get; set; }
        public string SearchString { get; set; }
    }

    internal class GetAllPagedPermissionsQueryHandler : IRequestHandler<GetAllPagedPermissionsQuery, PagedResult<GetAllPagedPermissionsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedPermissionsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<GetAllPagedPermissionsResponse>> Handle(GetAllPagedPermissionsQuery query, CancellationToken cancellationToken)
        {
            var assignedPermissions = await _unitOfWork.Repository<Domain.Entities.RolePermissions>()
                .Entities
                .Where(r => r.RoleId == query.RoleId)
                .Select(r => r.PermissionId)
                .ToArrayAsync();

            var permissions = _unitOfWork.Repository<Permission>()
                .Entities
                .Select(p => new GetAllPagedPermissionsResponse
                {
                    Id = p.Id,
                    GroupId = p.PermissionGroupId,
                    Name = p.Name,
                    Description = p.Description,
                    Assigned = assignedPermissions.Contains(p.Id),
                })
                .Where(p => p.GroupId == query.GroupId);

            if (!string.IsNullOrEmpty(query.SearchString))
            {
                permissions = permissions.Where(p => p.Name.Contains(query.SearchString) || p.Description.Contains(query.SearchString));
            }

            switch (query.SortLabel)
            {
                case "Name":
                    permissions = permissions.SortBy(r => r.Name, query.SortDirection);
                    break;

                case "Description":
                    permissions = permissions.SortBy(r => r.Description, query.SortDirection);
                    break;

                case "State":
                    permissions = permissions.SortBy(r => r.Assigned, query.SortDirection);
                    break;
            }

            return await permissions.ToPagedResultAsync(query.Page, query.PageSize);
        }
    }
}
