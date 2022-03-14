using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RolePermissions.Commands.Update
{
    public class UpdateRolePermissionsCommand : IRequest<Result<int[]>>
    {
        public int RoleId { get; set; }
        public IEnumerable<int> PermissionsIds { get; set; }
    }

    internal class UpdateRolePermissionsCommandHandler : IRequestHandler<UpdateRolePermissionsCommand, Result<int[]>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public UpdateRolePermissionsCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int[]>> Handle(UpdateRolePermissionsCommand command, CancellationToken cancellationToken)
        {
            var oldPermissions = _unitOfWork.Repository<Domain.Entities.RolePermissions>()
                .Entities
                .Where(rp => rp.RoleId == command.RoleId);

            await _unitOfWork.Repository<Domain.Entities.RolePermissions>()
                .RemoveRangeAsync(oldPermissions);

            var newPermissions = _unitOfWork.Repository<Permission>()
                .Entities
                .Where(p => command.PermissionsIds.Contains(p.Id))
                .Select(p => new Domain.Entities.RolePermissions
                {
                    PermissionId = p.Id,
                    RoleId = command.RoleId,
                });

            var result = await _unitOfWork.Repository<Domain.Entities.RolePermissions>()
                .AddRangeAsync(newPermissions);

            await _unitOfWork.Commit(cancellationToken);

            var ids = newPermissions.Select(p => p.Id).ToArray();

            return Result<int[]>.Success("Se actualizaron los permisos", ids);
        }
    }
}
