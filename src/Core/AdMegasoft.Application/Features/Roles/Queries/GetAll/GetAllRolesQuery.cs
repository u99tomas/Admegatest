using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Mappings;
using MediatR;

namespace AdMegasoft.Application.Features.Roles.Queries.GetAll
{
    public class GetAllRolesQuery : IRequest<IEnumerable<GetAllRolesResponse>>
    {
    }

    internal class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<GetAllRolesResponse>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<GetAllRolesResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.ToGetAllRolesResponse();
        }
    }
}
