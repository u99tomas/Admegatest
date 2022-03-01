namespace Application.Features.Permissions.Queries.ManagePermissions
{
    public class GetAllPagedPermissionsResponse
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Assigned { get; set; }
    }
}
