using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Roles.Queries.GetAll
{
    public class GetAllRolesQuery : IRequest<Result<List<GetAllRolesResponse>>>
    {

    }

    internal class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<List<GetAllRolesResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllRolesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAllRolesResponse>>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Repository<Role>()
                .Entities
                .Select(r => new GetAllRolesResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                })
                .ToListAsync();

            return Result<List<GetAllRolesResponse>>.Success(roles);
        }
    }
}
