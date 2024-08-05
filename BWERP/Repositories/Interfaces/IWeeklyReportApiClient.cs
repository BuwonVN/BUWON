using BWERP.Models.WeelkyReport;

namespace BWERP.Repositories.Interfaces
{
	public interface IWeeklyReportApiClient
	{
		//SAP - Vina System
		Task<List<InventorySummaryDto>> GetInventoryReportByRangeDate(DateRangeSearch daterangeSearch);
		Task<List<MBCPProductionDto>> Get_MBCP_ProductionByRangeDate(DateRangeSearch daterangeSearch);
		Task<List<MBCPMonthlyProdDto>> Get_MBCP_MonthlyProd(DateRangeSearch daterangeSearch);
		Task<List<OutstandingReceivableDto>> GetOutstandingReceivable(int year, int month);
		Task<List<OutstandingReceivableDetailDto>> GetOutstandingReceivable_Detail(int year, int month);
		Task<List<OutstandingReceivableByWeekDto>> GetOutstandingReceivableByWeek(int year, int month);
		//MES - Infoasia
		Task<List<OSProductionDto>> GetOSProduction(DateRangeSearch daterangeSearch);
		Task<List<IPOSProductionDto>> GetIPProduction(DateRangeSearch daterangeSearch);
		Task<List<IPOSMonthlyProdDto>> GetIPMonthlyProductions(DateRangeSearch daterangeSearch);
		Task<List<OSMonthlyProdDto>> GetOSMonthlyProductions(DateRangeSearch daterangeSearch);
		Task<List<ViewPlanningDto>> GetPlanningQty(DateRangeSearch daterangeSearch);
	}
}
