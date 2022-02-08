using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Application.Features.Users.Commands.AddEdit
{
    public class AddEditUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<int> RoleIds { get; set; }
    }

    internal class AddEditUserCommandHandler : IRequestHandler<AddEditUserCommand, int>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditUserCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddEditUserCommand command, CancellationToken cancellationToken)
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
                await _unitOfWork.CommitAsync(cancellationToken);

                var roles = command.RoleIds
                    .Select(id => new UserRoles { RoleId = id, UserId = user.Id })
                    .ToList();

                await _unitOfWork.Repository<UserRoles>().AddRangeAsync(roles);
                await _unitOfWork.CommitAsync(cancellationToken);

                return user.Id;
            }
            else
            {
                return command.Id;
            }
        }
    }
}
