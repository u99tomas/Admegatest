using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Groups.Queries.GetAll
{
    public class GetAllGroupsQuery : IRequest<Result<List<GetAllGroupsResponse>>>
    {

    }

    internal class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, Result<List<GetAllGroupsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllGroupsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAllGroupsResponse>>> Handle(GetAllGroupsQuery query, CancellationToken cancellationToken)
        {
            var groups = await _unitOfWork.Repository<PermissionGroup>()
                .Entities
                .Select(r => new GetAllGroupsResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                })
                .ToListAsync();

            return Result<List<GetAllGroupsResponse>>.Success(groups);
        }
    }
}
