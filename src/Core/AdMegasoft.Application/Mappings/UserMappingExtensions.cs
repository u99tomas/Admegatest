﻿using AdMegasoft.Application.Features.Users.Queries.GetAllPaged;
using AdMegasoft.Application.Models;
using AdMegasoft.Domain.Entities;

namespace AdMegasoft.Application.Mappings
{
    public static class UserMappingExtensions
    {
        public static UserModel ToModel(this User user, IEnumerable<RoleModel> roles, string accessToken)
        {
            return new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                AccessToken = accessToken,
                Roles = roles
            };
        }

        public static GetAllUsersResponse ToGetAllPagedUsersResponse(this User user)
        {
            return new GetAllUsersResponse
            {
                Name = user.Name,
                Password = user.Password,
            };
        }

        public static IEnumerable<GetAllUsersResponse> ToGetAllPagedUsersResponse(this IEnumerable<User> users)
        {
            return users.Select(u => u.ToGetAllPagedUsersResponse());
        }
    }
}
