using BWERP.Api.EF;
using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.RoleMenus;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BWERP.Api.Repositories.Services
{
	public class RoleMenusRepository : IRoleMenusRepository
	{
		private readonly MainContext _mainContext;
		public RoleMenusRepository(MainContext mainContext)
		{
			_mainContext = mainContext;
		}
		public async Task<PagedList<AppRoleMenu>> GetMenuByRoleId(Guid roleid, RoleMenuListSearch roleMenuListSearch)
		{
			var query = _mainContext.AppRoleMenus
				.Where(x => x.RoleId == roleid)
				.Include(x => x.AppRole)
				.Include(x => x.AppMenu).AsQueryable();

			var count = await query.CountAsync();

			var data = await query
			.Skip((roleMenuListSearch.PageNumber - 1) * roleMenuListSearch.PageSize)
			.Take(roleMenuListSearch.PageSize)
				.ToListAsync();
			return new PagedList<AppRoleMenu>(data, count, roleMenuListSearch.PageNumber, roleMenuListSearch.PageSize);
		}
		public async Task<AppRoleMenu> Create(AppRoleMenu roleMenu)
		{
			await _mainContext.AppRoleMenus.AddAsync(roleMenu);
			await _mainContext.SaveChangesAsync();
			return roleMenu;
		}

		public async Task<AppRoleMenu> Delete(AppRoleMenu roleMenu)
		{
			_mainContext.AppRoleMenus.Remove(roleMenu);
			await _mainContext.SaveChangesAsync();
			return roleMenu;
		}

		public async Task<AppRoleMenu> Update(AppRoleMenu roleMenu)
		{
			_mainContext.AppRoleMenus.Update(roleMenu);
			await _mainContext.SaveChangesAsync();
			return roleMenu;
		}

		public async Task<AppRoleMenu> GetById(int id)
		{
			return await _mainContext.AppRoleMenus.FindAsync(id);
		}

		public async Task<List<AppRole>> GetListRole()
		{
			return await _mainContext.Roles.ToListAsync();
		}
	}
}
