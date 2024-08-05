namespace BWERP.Api.Entities
{
    public class AppRoleMenu
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public AppRole AppRole { get; set; }

        public int MenuId { get; set; }
        public AppMenu AppMenu { get; set; }

        public List<AppMenuPermission> MenuPermissions { get; set; }
    }
}
