﻿namespace Application.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public IEnumerable<PermissionResponse> Permissions { get; set; }
    }
}
