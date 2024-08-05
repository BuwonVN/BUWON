using Microsoft.AspNetCore.Identity;

namespace BWERP.Api.Entities
{
	public class AppRole : IdentityRole<Guid>
	{
		public string? Description { get; set; }
        public List<AppRoleMenu> AppRoleMenus { get; set; }
    }
}
