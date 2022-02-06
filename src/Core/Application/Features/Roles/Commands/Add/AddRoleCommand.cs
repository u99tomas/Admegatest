using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Commands.Add
{
    public class AddRoleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, int>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddRoleCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddRoleCommand command, CancellationToken cancellationToken)
        {
            var newRole = new Role
            {
                Name = command.Name,
                Description = command.Description,
            };

            await _unitOfWork.Repository<Role>().AddAsync(newRole);

            return newRole.Id;
        }
    }
}
