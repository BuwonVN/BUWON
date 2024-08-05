using BWERP.Models.Role;
using BWERP.Models.Task;

namespace BWERP.Repositories.Interfaces
{
    public interface IRoleApiClient
    {
        Task<List<RoleViewDto>> GetListRole();
        Task<bool> AssignRole(Guid id, RoleAssignRequest request);
    }
}
