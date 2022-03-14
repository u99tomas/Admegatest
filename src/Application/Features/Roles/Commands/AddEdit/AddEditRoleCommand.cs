using Application.Interfaces.Repositories;
using Application.Mappings;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var exist = await _unitOfWork.Repository<Role>().Entities.AnyAsync(r => r.Name == command.Name && r.Id != command.Id);

            if (exist)
            {
                return Result<int>.Failure("El rol ya existe");
            }

            if (command.Id == 0)
            {
                var role = command.ToRole();

                await _unitOfWork.Repository<Role>().AddAsync(role);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success($"Se creo el Rol {role.Name}", role.Id);
            }
            else
            {
                var role = await _unitOfWork.Repository<Role>().GetByIdAsync(command.Id);

                if (role == null)
                {
                    return Result<int>.Failure($"No se ha encontrado el Rol");
                }

                role.Name = command.Name;
                role.Description = command.Description;

                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success($"Se actualizo el Rol {role.Name}", role.Id);
            }
        }
    }
}
