using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Commands.Delete
{
    public class DeleteRoleCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            var roleRepository = _unitOfWork.Repository<Role>();
            var foundRole = await roleRepository.GetByIdAsync(command.Id);

            if (foundRole == null)
            {
                return Result<int>.Failure($"Error: No se ha encontrado el Rol con Id {command.Id}");
            }

            await _unitOfWork.Repository<Role>().RemoveAsync(foundRole);
            await _unitOfWork.Commit(cancellationToken);

            return Result<int>.Success($"Se elimino el Rol {foundRole.Name}", foundRole.Id);
        }
    }
}
