using BWERP.Api.Entities;
using BWERP.Models.SeedWork;
using BWERP.Models.User;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<List<AppUser>> GetListAssignee();
		Task<PagedList<UserViewRequest>> GetListUser(int pageNumber, int pageSize);
		Task<AppUser> GetUserById(Guid id);
		Task<IEnumerable<UserRoleDto>> GetUserRole(Guid id);
		Task<AppUser> Create(AppUser user);
		Task<AppUser> Update(AppUser user);
		Task<AppUser> ChangePassword(AppUser user);
		Task<AppUser> Delete(AppUser user);
	}
}
