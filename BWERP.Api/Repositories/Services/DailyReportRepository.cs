using BWERP.Api.EF;
using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.Menu;
using BWERP.Models.SeedWork;
using BWERP.Models.Task;
using Microsoft.EntityFrameworkCore;

namespace BWERP.Api.Repositories.Services
{
	public class DailyReportRepository : IDailyReportRepository
	{
		private readonly MainContext _mainContext;
		public DailyReportRepository(MainContext mainContext)
		{
			_mainContext = mainContext;
		}

		public async Task<DailyReport> Create(DailyReport rep)
		{
			await _mainContext.DailyReports.AddAsync(rep);
			await _mainContext.SaveChangesAsync();
			return rep;
		}

		public async Task<DailyReport> Delete(DailyReport rep)
		{
			_mainContext.DailyReports.Remove(rep);
			await _mainContext.SaveChangesAsync();
			return rep;
		}

		public async Task<DailyReport> GetDailyReportById(int id)
		{
			return await _mainContext.DailyReports.FindAsync(id);
		}

		public async Task<PagedList<DailyReport>> GetListDailyReport(DailyReportListSearch dailyreportSearch)
		{
			//var query = _mainContext.DailyReports
			//	.Include(x => x.AppUser)
			//	.Include(x => x.Department).AsQueryable();

			//return await query.Where(s => s.UserId == userid).OrderByDescending(x => x.CreatedDate)
			//	.Skip(0).Take(10).ToListAsync();
			var query = _mainContext.DailyReports
				.Include(x => x.AppUser)
				.Include(x => x.Department).AsQueryable();

			if (!string.IsNullOrEmpty(dailyreportSearch.UserId))
				query = query.Where(t => t.UserId.ToString() == dailyreportSearch.UserId);

			var count = await query.CountAsync();

			var data = await query.OrderByDescending(x => x.CreatedDate)
			.Skip((dailyreportSearch.PageNumber - 1) * dailyreportSearch.PageSize)
			.Take(dailyreportSearch.PageSize)
			.ToListAsync();

			return new PagedList<DailyReport>(data, count, dailyreportSearch.PageNumber, dailyreportSearch.PageSize);
		}

		public async Task<List<DailyReport>> GetListDailyRptSearch(DailyReportListSearch dailyrptsearch)
		{
			DateTime searchDate = DateTime.Today;
			var query = _mainContext.DailyReports
				.Include(x => x.AppUser)
				.Include(x => x.Department).AsQueryable();

			query = query.Where(t => t.CreatedDate.Date == dailyrptsearch.CreatedDate);

			return await query.ToListAsync();
		}

		public async Task<DailyReport> Update(DailyReport rep)
		{
			_mainContext.DailyReports.Update(rep);
			await _mainContext.SaveChangesAsync();
			return rep;
		}
	}
}
