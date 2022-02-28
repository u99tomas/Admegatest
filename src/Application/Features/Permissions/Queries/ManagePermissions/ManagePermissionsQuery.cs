using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Permissions.Queries.ManagePermissions
{
    public class ManagePermissionsQuery : IRequest<Result<List<ManagePermissionsResponse>>>
    {
        public int RoleId { get; set; }
    }

    internal class ManagePermissionsQueryHandler : IRequestHandler<ManagePermissionsQuery, Result<List<ManagePermissionsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public ManagePermissionsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<ManagePermissionsResponse>>> Handle(ManagePermissionsQuery request, CancellationToken cancellationToken)
        {
            var assignedPermissions = await _unitOfWork.Repository<RolePermissions>()
                .Entities
                .Where(r => r.RoleId == request.RoleId)
                .Select(r => r.Id)
                .ToListAsync();

            var permissions = await _unitOfWork.Repository<Permission>()
                .Entities
                .Select(p => new ManagePermissionsResponse
                {
                    Id = p.Id,
                    GroupId = p.PermissionGroupId,
                    Name = p.Name,
                    Description = p.Description,
                    Assigned = assignedPermissions.Contains(p.Id),
                }).ToListAsync();

            return Result<List<ManagePermissionsResponse>>.Success(permissions);
        }
    }
}
