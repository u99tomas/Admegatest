using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Commands.Add
{
    public class AddEditRoleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class AddEditRoleCommandHandler : IRequestHandler<AddEditRoleCommand, int>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditRoleCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddEditRoleCommand command, CancellationToken cancellationToken)
        {
            if(command.Id == 0)
            {
                var newRole = new Role
                {
                    Name = command.Name,
                    Description = command.Description,
                };

                await _unitOfWork.Repository<Role>().AddAsync(newRole);
                await _unitOfWork.CommitAsync(cancellationToken);

                return newRole.Id;
            }
            else
            {
                var foundRole = await _unitOfWork.Repository<Role>().GetByIdAsync(command.Id);

                foundRole.Name = command.Name;
                foundRole.Description = command.Description;

                await _unitOfWork.Repository<Role>().UpdateAsync(foundRole);
                await _unitOfWork.CommitAsync(cancellationToken);
                return foundRole.Id;
            }
        }
    }
}
