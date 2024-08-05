using BWERP.Api.Entities;
using BWERP.Models.Menu;
using BWERP.Models.SeedWork;

namespace BWERP.Api.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        Task<PagedList<AppMenu>> GetAllMenu(MenuListSearch taskListSearch);
        //Task<PagedList<Task>> GetByUserId(Guid userid, TaskListSearch taskListSearch);
        Task<List<AppMenu>> GetParentMenu();
        Task<List<MenuViewRequest>> GetMenuByUser(string username);
        Task<AppMenu> GetMenuById(int id);
        Task<AppMenu> Create(AppMenu menu);
        Task<AppMenu> Update(AppMenu menu);
        Task<AppMenu> Delete(AppMenu menu);
    }
}
