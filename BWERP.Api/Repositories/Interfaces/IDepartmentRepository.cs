using BWERP.Api.Entities;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IDepartmentRepository
	{
		Task<List<Department>> GetDepartmentList();
		Task<List<Department>> GetDepartmentByUser(Guid userid);
	}
}
