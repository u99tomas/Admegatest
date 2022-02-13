using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Application.Features.Users.Commands.AddEdit
{
    public class AddEditUserCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
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
            if (command.Id == 0)
            {
                var user = new User
                {
                    AccountName = command.AccountName,
                    Password = command.Password,
                    IsActive = command.IsActive,
                };

                await _unitOfWork.Repository<User>().AddAsync(user);

                var roles = command.RoleIds
                    .Select(id => new UserRoles { RoleId = id, User = user })
                    .ToList();

                await _unitOfWork.Repository<UserRoles>().AddRangeAsync(roles);
                await _unitOfWork.CommitAsync(cancellationToken);

                return Result<int>.Success($"Se creo el usuario {user.AccountName}", user.Id);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
