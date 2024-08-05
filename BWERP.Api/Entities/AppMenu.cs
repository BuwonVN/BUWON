namespace BWERP.Api.Entities
{
    public class AppMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? ParentId { get; set; }
        public string? Icon { get; set; }
        public string? IconPath { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public int isEnable { get; set; }
        public List<AppRoleMenu> AppRoleMenus { get; set; }
    }
}
