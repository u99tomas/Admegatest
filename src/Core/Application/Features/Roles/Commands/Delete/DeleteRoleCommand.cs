using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Commands.Delete
{
    public class DeleteRoleCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    internal class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, int>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteRoleCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            var roleRepository = _unitOfWork.Repository<Role>();
            var foundRole = await roleRepository.GetByIdAsync(command.Id);

            await _unitOfWork.Repository<Role>().DeleteAsync(foundRole);
            await _unitOfWork.CommitAsync(cancellationToken);

            return command.Id;
        }
    }
}
