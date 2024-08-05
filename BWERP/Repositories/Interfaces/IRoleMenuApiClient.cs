using BWERP.Models.Menu;
using BWERP.Models.Role;
using BWERP.Models.RoleMenus;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;

namespace BWERP.Repositories.Interfaces
{
	public interface IRoleMenuApiClient
	{
		Task<PagedList<RoleMenusDto>> GetListMenu(Guid roleid,RoleMenuListSearch listSearch);
		Task<List<RoleViewDto>> GetListRole();
	}
}
