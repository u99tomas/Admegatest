﻿using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.AddEdit
{
    public class AddEditUserCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<int> RoleIds { get; set; }
    }

    internal class AddEditUserCommandHandler : IRequestHandler<AddEditUserCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditUserCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(AddEditUserCommand command, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Repository<User>().Entities.AnyAsync(u => u.Name == command.Name);

            if (exist)
            {
                return Result<int>.Failure("El usuario ya existe");
            }

            if (command.Id == 0)
            {
                var user = new User
                {
                    Name = command.Name,
                    Password = command.Password,
                    IsActive = command.IsActive,
                };

                await _unitOfWork.Repository<User>().AddAsync(user);

                var roles = command.RoleIds
                    .Select(id => new UserRoles { RoleId = id, User = user })
                    .ToList();

                await _unitOfWork.Repository<UserRoles>().AddRangeAsync(roles);
                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success($"Se creo el usuario {user.Name}", user.Id);
            }
            else
            {
                var user = await _unitOfWork.Repository<User>().GetByIdAsync(command.Id);

                if (user == null)
                {
                    return Result<int>.Failure($"No se hallo el usuario");
                }

                user.Name = command.Name;
                user.Password = command.Password;
                user.IsActive = command.IsActive;

                var userRoles = await _unitOfWork.Repository<UserRoles>()
                    .Entities
                    .Where(u => u.UserId == command.Id)
                    .ToListAsync();

                await _unitOfWork.Repository<UserRoles>().RemoveRangeAsync(userRoles);

                var newUserRoles = command.RoleIds.Select(id => new UserRoles { UserId = user.Id, RoleId = id })
                    .ToList();

                await _unitOfWork.Repository<UserRoles>().AddRangeAsync(newUserRoles);

                await _unitOfWork.Commit(cancellationToken);

                return Result<int>.Success($"Se actualizo el usuario {user.Name}", user.Id);
            }
        }
    }
}
