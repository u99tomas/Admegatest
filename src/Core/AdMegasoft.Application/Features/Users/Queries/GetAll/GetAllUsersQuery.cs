using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Mappings;
using MediatR;

namespace AdMegasoft.Application.Features.Users.Queries.GetAllPaged
{
    public class GetAllUsersQuery : IRequest<IEnumerable<GetAllUsersResponse>>
    {
    }

    internal class GetAllPagedUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllPagedUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetAllUsersResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return users.ToGetAllPagedUsersResponse();
        }
    }
}
