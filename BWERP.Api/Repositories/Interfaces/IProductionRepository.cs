using BWERP.Models.Production;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IProductionRepository
	{
		Task<IEnumerable<ProductionViewDto>> GetProductionStatus_Dashboard(string dept, DateTime scanDate);
	}
}
