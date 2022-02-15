using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Queries.GetRolesIdsOfUser
{
    public class GetRolesIdsOfUserQuery : IRequest<Result<List<int>>>
    {
        public int UserId { get; set; }
    }

    internal class GetAllRolesQueryHandler : IRequestHandler<GetRolesIdsOfUserQuery, Result<List<int>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllRolesQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<int>>> Handle(GetRolesIdsOfUserQuery query, CancellationToken cancellationToken)
        {
            var ids = await _unitOfWork.Repository<UserRoles>()
                .Entities
                .Where(ur => ur.UserId == query.UserId)
                .Select(ur => ur.RoleId)
                .Distinct()
                .ToListAsync();

            return Result<List<int>>.Success(ids);
        }
    }
}
