using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Mappings;
using MediatR;

namespace AdMegasoft.Application.Features.Users.Queries.GetAllPaged
{
    public class GetAllPagedUsersQuery : IRequest<List<GetAllPagedUsersResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    internal class GetAllPagedUsersQueryHandler : IRequestHandler<GetAllPagedUsersQuery, List<GetAllPagedUsersResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllPagedUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<GetAllPagedUsersResponse>> Handle(GetAllPagedUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return users.ToGetAllPagedUsersResponse();
        }
    }
}
