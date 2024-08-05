using BWERP.Api.EF;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.Role;
using BWERP.Models.WeelkyReport;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BWERP.Api.Repositories.Services
{
	public class WeeklyReportRepository : IWeeklyReportRepository
	{
		//Using Dapper
		//TEST ADO.NET
		private readonly IConfiguration _configuration;
		private readonly string _constrSAP;
		private readonly string _constrMES;
		private readonly string _constrSAPReport;
		public WeeklyReportRepository(IConfiguration configuration)
		{
			_configuration = configuration;
			_constrSAP = configuration.GetConnectionString("SAPDatabase");
			_constrMES = configuration.GetConnectionString("MESDatabase");
			_constrSAPReport = configuration.GetConnectionString("SAPReportDatabase");
		}
		private IDbConnection sqlconSAP => new SqlConnection(_constrSAP);
		private IDbConnection sqlconMES => new SqlConnection(_constrMES);
		private IDbConnection sqlconReportSAP => new SqlConnection(_constrSAPReport);

		public async Task<IEnumerable<InventorySummaryDto>> GetInventoryReportByRangeDate(DateTime fromDate, DateTime toDate)
		{
			using (var db = sqlconSAP)
			{
				var parameters = new { fromDate, toDate };
				return await db.QueryAsync<InventorySummaryDto>("VNS_RPT_INV_SUM_KR", parameters, commandType: CommandType.StoredProcedure);
			}
		}
		//Get IPOS Monthly Production
		public async Task<IEnumerable<IPOSMonthlyProdDto>> GetMonthlyProdByDept(string dept, string year)
		{
			using (var db = sqlconMES)
			{
				var parameters = new { dept, year};
				return await db.QueryAsync<IPOSMonthlyProdDto>("spGetMonthlyProductionByDept", parameters, commandType: CommandType.StoredProcedure);
			}
		}
		//Get Outstanding Receivable
		public async Task<IEnumerable<OutstandingReceivableDto>> GetOutstandingReceivable(int year, int month)
		{
			using (var db = sqlconReportSAP)
			{
				var parameters = new { year, month };
				return await db.QueryAsync<OutstandingReceivableDto>("spWeeklyReport_OutstandingReceivable",parameters, commandType: CommandType.StoredProcedure);
			}
		}

		public async Task<IEnumerable<OutstandingReceivableByWeekDto>> GetOutstandingReceivableByWeek(int year, int month)
		{
			using (var db = sqlconReportSAP)
			{
				var parameters = new { year, month };
				return await db.QueryAsync<OutstandingReceivableByWeekDto>("spWeeklyReport_OutstandingReceivableByWeek", parameters, commandType: CommandType.StoredProcedure);
			}
		}

		public async Task<IEnumerable<OutstandingReceivableDetailDto>> GetOutstandingReceivable_Detail(int year, int month)
		{
			using (var db = sqlconReportSAP)
			{
				var parameters = new { year, month };
				return await db.QueryAsync<OutstandingReceivableDetailDto>("spWeeklyReport_OutstandingReceivable_Detail", parameters, commandType: CommandType.StoredProcedure);
			}
		}

		//Get Production Plan
		public async Task<IEnumerable<ViewPlanningDto>> GetPlanningQty(DateTime fromdate, DateTime todate)
		{
			using (var db = sqlconMES)
			{
				var parameters = new { fromdate, todate };
				return await db.QueryAsync<ViewPlanningDto>("spGetPlanQuantity", parameters, commandType: CommandType.StoredProcedure);
			}
		}

		//Get IPOS Production
		public async Task<IEnumerable<IPOSProductionDto>> GetProductionByDept(string dept,  DateTime fromdate, DateTime todate)
		{
			using (var db = sqlconMES)
			{
				var parameters = new { dept, fromdate, todate };
				return await db.QueryAsync<IPOSProductionDto>("spGetProductionByDept", parameters, commandType: CommandType.StoredProcedure);
			}
		}
		//Get MBCP monthly production
		public async Task<IEnumerable<MBCPMonthlyProdDto>> Get_MBCP_MonthlyProd(string year)
		{
			using (var db = sqlconSAP)
			{
				var parameters = new { year };
				return await db.QueryAsync<MBCPMonthlyProdDto>("spGetMonthlyProductionByDept", parameters, commandType: CommandType.StoredProcedure);
			}
		}
		//Get MBCP production
		public async Task<IEnumerable<MBCPProductionDto>> Get_MBCP_ProductionByRangeDate(DateTime fromdate, DateTime todate)
		{
			using (var db = sqlconSAP)
			{
				var parameters = new { fromdate, todate };
				return await db.QueryAsync<MBCPProductionDto>("spGetProductionByDept", parameters, commandType: CommandType.StoredProcedure);
			}
		}
	}
}
