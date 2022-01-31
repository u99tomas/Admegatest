using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Mappings;
using MediatR;

namespace AdMegasoft.Application.Features.Groups.Queries.GetAll
{
    public class GetAllGroupsQuery : IRequest<IEnumerable<GetAllGroupsResponse>>
    {
    }

    internal class GetAllGroupsHandler : IRequestHandler<GetAllGroupsQuery, IEnumerable<GetAllGroupsResponse>>
    {
        private readonly IGroupRepository _groupRepository;

        public GetAllGroupsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<GetAllGroupsResponse>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAllAsync();
            return groups.ToGetAllGroupsResponse();
        }
    }
}
