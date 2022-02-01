using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Mappings;
using MediatR;

namespace AdMegasoft.Application.Features.Roles.Commands.AddEdit
{
    public class AddEditRoleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class AddEditRoleCommandHandler : IRequestHandler<AddEditRoleCommand, int>
    {
        private readonly IRoleRepository _roleRepository;

        public AddEditRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<int> Handle(AddEditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = request.ToRole();
            await _roleRepository.AddOrUpdateAsync(role);
            return role.Id;
        }
    }
}
