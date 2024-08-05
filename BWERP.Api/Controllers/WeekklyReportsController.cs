using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.WeelkyReport;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeekklyReportsController : ControllerBase
	{
		private readonly IWeeklyReportRepository _weeklyreport;

		public WeekklyReportsController(IWeeklyReportRepository weeklyreport)
		{
			_weeklyreport = weeklyreport;
		}

		// GET: api/weelyreports/inventorystatus
		[HttpGet("inventorystatus")]
		public async Task<ActionResult<List<InventorySummaryDto>>> GetInventoryReportByRangeDate(DateTime fromDate, DateTime toDate)
		{
			var products = await _weeklyreport.GetInventoryReportByRangeDate(fromDate, toDate);
			return Ok(products);
		}
		//Get Outstanding Receivable
		[HttpGet("outstanding")]
		public async Task<ActionResult<List<OutstandingReceivableDto>>> GetOutstandingReceivable(int year, int  month)
		{
			var products = await _weeklyreport.GetOutstandingReceivable(year, month);
			return Ok(products);
		}
		//Get Outstanding Receivable
		[HttpGet("outstandingdetail")]
		public async Task<ActionResult<List<OutstandingReceivableDto>>> GetOutstandingReceivableDetail(int year, int month)
		{
			var products = await _weeklyreport.GetOutstandingReceivable_Detail(year, month);
			return Ok(products);
		}
		//Get Outstanding Receivable by week
		[HttpGet("outstandingbyweek")]
		public async Task<ActionResult<List<OutstandingReceivableDto>>> GetOutstandingReceivableByWeek(int year, int month)
		{
			var products = await _weeklyreport.GetOutstandingReceivableByWeek(year, month);
			return Ok(products);
		}
		// GET: api/weelyreports/mbcpprodstatus
		[HttpGet("mbcpprodstatus")]
		public async Task<ActionResult<List<MBCPProductionDto>>> Get_MBCP_ProductionByRangeDate(DateTime fromDate, DateTime toDate)
		{
			var products = await _weeklyreport.Get_MBCP_ProductionByRangeDate(fromDate, toDate);
			return Ok(products);
		}
		// GET: api/weelyreports/mbcpmonthlyprod
		[HttpGet("mbcpmonthlyprod")]
		public async Task<ActionResult<List<MBCPMonthlyProdDto>>> Get_MBCP_ProductionByRangeDate(string year)
		{
			var products = await _weeklyreport.Get_MBCP_MonthlyProd(year);
			return Ok(products);
		}
		//Get IP OS Production
		[HttpGet("productionstatus")]
		public async Task<ActionResult<List<IPOSProductionDto>>> GetProductionByDept(string dept, DateTime fromdate, DateTime todate)
		{
			var products = await _weeklyreport.GetProductionByDept(dept, fromdate, todate);
			return Ok(products);
		}
		//Get IP OS Production by month
		[HttpGet("monthlyprod")]
		public async Task<ActionResult<List<IPOSMonthlyProdDto>>> GetMonthlyProductionByDept(string dept, string year)
		{
			var products = await _weeklyreport.GetMonthlyProdByDept(dept, year);
			return Ok(products);
		}
		//Get Production Plan
		[HttpGet("prodplan")]
		public async Task<ActionResult<List<ViewPlanningDto>>> GetPlanningQty(DateTime fromdate, DateTime todate)
		{
			var products = await _weeklyreport.GetPlanningQty(fromdate, todate);
			return Ok(products);
		}
	}
}
