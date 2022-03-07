namespace Application.Features.Permissions.Queries.ManagePermissions
{
    public class ManagePermissionsPermissionsResponse
    {
        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Assigned { get; set; }
    }
}
