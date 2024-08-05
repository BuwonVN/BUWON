namespace BWERP.Api.Entities
{
    public class AppMenuPermission
    {
        public int RoleMenuId { get; set; }
        public AppRoleMenu AppRoleMenu { get; set; }
        public int PermissionId { get; set; }
        public AppPermission AppPermission { get; set; }
    }
}
