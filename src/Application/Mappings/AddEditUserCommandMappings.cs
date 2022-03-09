using Application.Features.Users.Commands.AddEdit;
using Domain.Entities;

namespace Application.Mappings
{
    public static class AddEditUserCommandMappings
    {
        public static User ToUser(this AddEditUserCommand userCommand)
        {
            return new User
            {
                Id = userCommand.Id,
                Name = userCommand.Name,
                IsActive = userCommand.IsActive,
                Password = userCommand.Password,
            };
        }
    }
}
