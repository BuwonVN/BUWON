using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;

namespace BWERP.Repositories.Interfaces
{
	public interface IMenuApiClient
	{
		Task<PagedList<MenuViewRequest>> GetListMenu(MenuListSearch menuListSearch);
		Task<List<ParentMenuDto>> GetParentMenu();
		Task<MenuViewRequest> GetMenuById(int id);
        Task<List<MenuViewRequest>> GetMenuByUser(string username);
        Task<bool> CreateMenu(MenuCreateRequest request);
		Task<bool> UpdateMenu(int id, MenuUpdateRequest request);
		Task<bool> DeleteMenu(int id);
	}
}
