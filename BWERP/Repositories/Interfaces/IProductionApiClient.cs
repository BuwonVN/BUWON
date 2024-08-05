using BWERP.Models.Production;
using BWERP.Models.WeelkyReport;

namespace BWERP.Repositories.Interfaces
{
	public interface IProductionApiClient
	{
		Task<List<ProductionViewDto>> GetProductionStatus_Dashboard(string dept, DateTime scandate);
	}
}
