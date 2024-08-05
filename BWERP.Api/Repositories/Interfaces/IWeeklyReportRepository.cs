using BWERP.Models.WeelkyReport;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IWeeklyReportRepository
	{
		//SAP - Vina System
		Task<IEnumerable<InventorySummaryDto>> GetInventoryReportByRangeDate(DateTime fromdate, DateTime todate);
		Task<IEnumerable<MBCPProductionDto>> Get_MBCP_ProductionByRangeDate(DateTime fromdate, DateTime todate);
		Task<IEnumerable<MBCPMonthlyProdDto>> Get_MBCP_MonthlyProd(string year);
		Task<IEnumerable<OutstandingReceivableDto>> GetOutstandingReceivable(int year, int month);
		Task<IEnumerable<OutstandingReceivableDetailDto>> GetOutstandingReceivable_Detail(int year, int month);
		Task<IEnumerable<OutstandingReceivableByWeekDto>> GetOutstandingReceivableByWeek(int year, int month);

		//SAP - Infoasia
		Task<IEnumerable<IPOSProductionDto>> GetProductionByDept(string dept, DateTime fromdate, DateTime todate);
		Task<IEnumerable<IPOSMonthlyProdDto>> GetMonthlyProdByDept(string dept, string year);
		Task<IEnumerable<ViewPlanningDto>> GetPlanningQty(DateTime fromdate, DateTime todate);
	}
}
