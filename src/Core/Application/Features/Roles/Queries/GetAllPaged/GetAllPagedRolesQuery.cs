﻿using Application.Interfaces.Repositories;
using Application.Mappings;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Roles.Queries.GetAllPaged
{
    public class GetAllPagedRolesQuery : IRequest<PagedResult<GetAllPagedRolesResponse>>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        /// <summary>
        /// "Ascending" ordena de forma ascendente <br></br>
        /// "Descending" ordena de forma descendente
        /// </summary>
        public string SortDirection { get; set; }
        public string SortLabel { get; set; }
        public string SearchString { get; set; }
    }

    internal class GetAllPagedRolesHandler : IRequestHandler<GetAllPagedRolesQuery, PagedResult<GetAllPagedRolesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllPagedRolesHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<GetAllPagedRolesResponse>> Handle(GetAllPagedRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = _unitOfWork.Repository<Role>().Entities;

            if (!string.IsNullOrEmpty(query.SearchString))
            {
                roles = roles.Where(
                    r => r.Name.Contains(query.SearchString) ||
                    r.Description.Contains(query.SearchString)
                );
            }

            switch (query.SortLabel)
            {
                case "Name":
                    roles = roles.SortBy(r => r.Name, query.SortDirection);
                    break;

                case "Description":
                    roles = roles.SortBy(r => r.Description, query.SortDirection);
                    break;
            }

            return await roles.Select(r =>
                new GetAllPagedRolesResponse
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                }).ToPagedResponseAsync(query.Page, query.PageSize);
        }
    }
}
