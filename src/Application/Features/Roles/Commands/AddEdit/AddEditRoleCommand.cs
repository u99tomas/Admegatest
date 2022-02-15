using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Commands.Add
{
    public class AddEditRoleCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class AddEditRoleCommandHandler : IRequestHandler<AddEditRoleCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditRoleCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(AddEditRoleCommand command, CancellationToken cancellationToken)
        {
            if(command.Id == 0)
            {
                var newRole = new Role
                {
                    Name = command.Name,
                    Description = command.Description,
                };

                await _unitOfWork.Repository<Role>().AddAsync(newRole);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<int>.Success($"Se creo el Rol {newRole.Name}", newRole.Id);
            }
            else
            {
                var foundRole = await _unitOfWork.Repository<Role>().GetByIdAsync(command.Id);

                if(foundRole == null)
                {
                    return Result<int>.Failure($"Error: No se ha encontrado el Rol con Id {command.Id}");
                }

                foundRole.Name = command.Name;
                foundRole.Description = command.Description;
                
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result<int>.Success($"Se actualizo el Rol {foundRole.Name}", foundRole.Id);
            }
        }
    }
}
