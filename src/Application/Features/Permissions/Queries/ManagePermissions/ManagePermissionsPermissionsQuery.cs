using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Permissions.Queries.ManagePermissions
{
    public class ManagePermissionsPermissionsQuery : IRequest<Result<List<ManagePermissionsPermissionsResponse>>>
    {
        public int RoleId { get; set; }
    }

    internal class GetAllPagedPermissionsQueryHandler : IRequestHandler<ManagePermissionsPermissionsQuery, Result<List<ManagePermissionsPermissionsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedPermissionsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ManagePermissionsPermissionsResponse>>> Handle(ManagePermissionsPermissionsQuery query, CancellationToken cancellationToken)
        {
            var assignedPermissions = await _unitOfWork.Repository<Domain.Entities.RolePermissions>()
                .Entities
                .Where(r => r.RoleId == query.RoleId)
                .Select(r => r.PermissionId)
                .ToArrayAsync();

            var permissions = await _unitOfWork.Repository<Permission>()
                .Entities
                .Select(p => new ManagePermissionsPermissionsResponse
                {
                    PermissionId = p.Id,
                    GroupId = p.PermissionGroupId,
                    Name = p.Name,
                    Description = p.Description,
                    Assigned = assignedPermissions.Contains(p.Id),
                }).ToListAsync();

            return Result<List<ManagePermissionsPermissionsResponse>>.Success(permissions);
        }
    }
}
