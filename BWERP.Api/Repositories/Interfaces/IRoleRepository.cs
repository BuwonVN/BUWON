using BWERP.Api.Entities;
using BWERP.Models.Role;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IRoleRepository
	{
        Task<List<RoleViewDto>> GetListRole();
        Task<List<RolePermissionDtos>> GetRolePermission(Guid roleId);

	}
}
