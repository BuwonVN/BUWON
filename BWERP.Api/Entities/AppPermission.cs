namespace BWERP.Api.Entities
{
    public class AppPermission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AppMenuPermission> MenuPermissions { get; set; }
    }
}
