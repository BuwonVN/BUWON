using BWERP.Api.EF;
using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.Repositories.Services
{
    public class MenuRepository : IMenuRepository
    {
        private readonly MainContext _mainContext;
        public MenuRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task<AppMenu> GetMenuById(int id)
        {
            return await _mainContext.Menus.FindAsync(id);
        }

        public async Task<PagedList<AppMenu>> GetAllMenu(MenuListSearch menuListSearch)
        {
            var query = from m in _mainContext.Menus
                            //.Include(x => x.Assignee).AsQueryable();
                        select m;
            if (!string.IsNullOrEmpty(menuListSearch.Name))
                query = query.Where(t => t.Name.Contains(menuListSearch.Name));

            var count = await query.CountAsync();

            var data = await query.OrderBy(x => x.SortOrder)
                .Skip((menuListSearch.PageNumber - 1) * menuListSearch.PageSize)
                .Take(menuListSearch.PageSize)
                .ToListAsync(); 
            return new PagedList<AppMenu>(data, count, menuListSearch.PageNumber, menuListSearch.PageSize);
        }
        public async Task<AppMenu> Create(AppMenu menu)
        {
            await _mainContext.Menus.AddAsync(menu);
            await _mainContext.SaveChangesAsync();
            return menu;
        }

        public async Task<AppMenu> Delete(AppMenu menu)
        {
            _mainContext.Menus.Remove(menu);
            await _mainContext.SaveChangesAsync();
            return menu;
        }

        public async Task<AppMenu> Update(AppMenu menu)
        {
            _mainContext.Menus.Update(menu);
            await _mainContext.SaveChangesAsync();
            return menu;
        }

		public async Task<List<AppMenu>> GetParentMenu()
		{
			return await _mainContext.Menus.Where(x=> x.ParentId == 0).ToListAsync();
		}

        public async Task<List<MenuViewRequest>> GetMenuByUser(string username)
        {
            var query = from menu in _mainContext.Menus
                        join rolemenu in _mainContext.AppRoleMenus on menu.Id equals rolemenu.MenuId
                        join userrole in _mainContext.UserRoles on rolemenu.RoleId equals userrole.RoleId
                        join user in _mainContext.Users on userrole.UserId equals user.Id
                        where user.UserName == username
                        select menu;
            return await query.Select(x => new MenuViewRequest()
            {
                Id = x.Id,
                Name= x.Name,
                Description= x.Description,
                ParentId= x.ParentId,
                Icon = x.Icon,
                IconPath = x.IconPath,
                Url= x.Url,
                SortOrder= x.SortOrder
            }).OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
