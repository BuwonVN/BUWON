using BWERP.Api.Entities;
using BWERP.Models.RoleMenus;
using BWERP.Models.SeedWork;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IRoleMenusRepository
	{
		Task<AppRoleMenu> GetById(int id);
		Task<PagedList<AppRoleMenu>> GetMenuByRoleId(Guid roleid, RoleMenuListSearch roleMenuListSearch);
		Task<List<AppRole>> GetListRole();
		Task<AppRoleMenu> Create(AppRoleMenu roleMenu);
		Task<AppRoleMenu> Update(AppRoleMenu roleMenu);
		Task<AppRoleMenu> Delete(AppRoleMenu roleMenu);
	}
}
