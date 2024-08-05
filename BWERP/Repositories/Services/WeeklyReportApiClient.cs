using BWERP.Models.Comment;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.WeelkyReport;
using BWERP.Repositories.Interfaces;
using System;

namespace BWERP.Repositories.Services
{
	public class WeeklyReportApiClient : IWeeklyReportApiClient
	{
		public HttpClient _httpClient;
		public WeeklyReportApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		//MES API
		//Get IP Production
		public async Task<List<IPOSProductionDto>> GetIPProduction(DateRangeSearch daterangeSearch)
		{
			string url = $"/api/WeekklyReports/productionstatus?dept=590&fromDate={daterangeSearch.FromDate.Value.ToString("yyyy-MM-dd")}&toDate={daterangeSearch.ToDate.Value.ToString("yyyy-MM-dd")}";

			var result = await _httpClient.GetFromJsonAsync<List<IPOSProductionDto>>(url);
			return result;
		}
		//Get OS Production
		public async Task<List<OSProductionDto>> GetOSProduction(DateRangeSearch daterangeSearch)
		{
			string url = $"/api/WeekklyReports/productionstatus?dept=510&fromDate={daterangeSearch.FromDate.Value.ToString("yyyy-MM-dd")}&toDate={daterangeSearch.ToDate.Value.ToString("yyyy-MM-dd")}";

			var result = await _httpClient.GetFromJsonAsync<List<OSProductionDto>>(url);
			return result;
		}

		public async Task<List<IPOSMonthlyProdDto>> GetIPMonthlyProductions(DateRangeSearch daterangeSearch)
		{
			string url = $"/api/WeekklyReports/monthlyprod?dept=590&year={daterangeSearch.FromDate.Value.ToString("yyyy")}";

			var result = await _httpClient.GetFromJsonAsync<List<IPOSMonthlyProdDto>>(url);
			return result;
		}
		public async Task<List<OSMonthlyProdDto>> GetOSMonthlyProductions(DateRangeSearch daterangeSearch)
		{
			string url = $"/api/WeekklyReports/monthlyprod?dept=510&year={daterangeSearch.FromDate.Value.ToString("yyyy")}";

			var result = await _httpClient.GetFromJsonAsync<List<OSMonthlyProdDto>>(url);
			return result;
		}
		//SAP API
		//Get Inventory report API
		public async Task<List<InventorySummaryDto>> GetInventoryReportByRangeDate(DateRangeSearch daterangeSearch)
		{
			string url = $"/api/WeekklyReports/inventorystatus?fromDate={daterangeSearch.FromDate.Value.ToString("yyyy-MM-dd")}&toDate={daterangeSearch.ToDate.Value.ToString("yyyy-MM-dd")}";

			var result = await _httpClient.GetFromJsonAsync<List<InventorySummaryDto>>(url);
			return result;
		}
		//Get MBCP Production API
		public async Task<List<MBCPProductionDto>> Get_MBCP_ProductionByRangeDate(DateRangeSearch daterangeSearch)
		{
			string url = $"/api/WeekklyReports/mbcpprodstatus?fromDate={daterangeSearch.FromDate.Value.ToString("yyyy-MM-dd")}&toDate={daterangeSearch.ToDate.Value.ToString("yyyy-MM-dd")}";

			var result = await _httpClient.GetFromJsonAsync<List<MBCPProductionDto>>(url);
			return result;
		}
		//Get MBCP monthly production API
		public async Task<List<MBCPMonthlyProdDto>> Get_MBCP_MonthlyProd(DateRangeSearch daterangeSearch)
		{
			string url = $"/api/WeekklyReports/mbcpmonthlyprod?year={daterangeSearch.FromDate.Value.ToString("yyyy")}";

			var result = await _httpClient.GetFromJsonAsync<List<MBCPMonthlyProdDto>>(url);
			return result;
		}
		//Get Outstanding Receivable API
		public async Task<List<OutstandingReceivableDto>> GetOutstandingReceivable(int year, int month)
		{
			string url = $"/api/WeekklyReports/outstanding?year={year}&month={month}";

			var result = await _httpClient.GetFromJsonAsync<List<OutstandingReceivableDto>>(url);
			return result;
		}
		//Get Outstanding Receivable Detail API
		public async Task<List<OutstandingReceivableDetailDto>> GetOutstandingReceivable_Detail(int year, int month)
		{
			string url = $"/api/WeekklyReports/outstandingdetail?year={year}&month={month}";

			var result = await _httpClient.GetFromJsonAsync<List<OutstandingReceivableDetailDto>>(url);
			return result;
		}
		//Get Outstanding Receivable by week API
		public async Task<List<OutstandingReceivableByWeekDto>> GetOutstandingReceivableByWeek(int year, int month)
		{
			string url = $"/api/WeekklyReports/outstandingbyweek?year={year}&month={month}";

			var result = await _httpClient.GetFromJsonAsync<List<OutstandingReceivableByWeekDto>>(url);
			return result;
		}
		public async Task<List<ViewPlanningDto>> GetPlanningQty(DateRangeSearch daterangeSearch)
		{
			string url = $"/api/WeekklyReports/prodplan?fromdate={daterangeSearch.FromDate.Value.ToString("yyyy-MM-dd")}&todate={daterangeSearch.ToDate.Value.ToString("yyyy-MM-dd")}";

			var result = await _httpClient.GetFromJsonAsync<List<ViewPlanningDto>>(url);
			return result;
		}
	}
}
