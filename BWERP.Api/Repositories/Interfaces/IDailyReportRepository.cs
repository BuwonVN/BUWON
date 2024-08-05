using BWERP.Api.Entities;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;

namespace BWERP.Api.Repositories.Interfaces
{
	public interface IDailyReportRepository
	{
		Task<PagedList<DailyReport>> GetListDailyReport(DailyReportListSearch dailyreportSearch);
		Task<List<DailyReport>> GetListDailyRptSearch(DailyReportListSearch dailyrptsearch);
		Task<DailyReport> GetDailyReportById(int id);
		Task<DailyReport> Create(DailyReport user);
		Task<DailyReport> Update(DailyReport user);
		Task<DailyReport> Delete(DailyReport user);
	}
}
