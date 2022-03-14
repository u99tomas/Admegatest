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
                Enabled = userCommand.Enabled,
                Password = userCommand.Password,
            };
        }
    }
}
