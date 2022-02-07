using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Roles.Queries.GetAll
{
    public class GetAllRolesQuery : IRequest<List<GetAllRolesResponse>>
    {

    }

    internal class GetAllPagedRolesHandler : IRequestHandler<GetAllRolesQuery, List<GetAllRolesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedRolesHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllRolesResponse>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
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

            return roles;
        }
    }
}
