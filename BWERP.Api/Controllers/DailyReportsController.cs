using BWERP.Api.Entities;
using BWERP.Api.Repositories.Interfaces;
using BWERP.Models.DepartmentDailyReport;
using BWERP.Models.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace BWERP.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DailyReportsController : ControllerBase
	{
		private readonly IDailyReportRepository _dailyReport;
		public DailyReportsController(IDailyReportRepository dailyReport)
		{
			_dailyReport = dailyReport;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllReport([FromQuery] DailyReportListSearch dailyreportSearch)
		{
			//var users = await _dailyReport.GetListDailyReport(userid);

			//var userDtos = users.Select(x => new DailyReportView()
			//{
			//	Id = x.Id,
			//	TodayTask = x.TodayTask,
			//	TomorrowTask = x.TomorrowTask,
			//	CreatedBy = x.AppUser.UserName,
			//	CreatedDate = x.CreatedDate,
			//	UpdatedDate = x.UpdatedDate,
			//	DepartmentName = x.Department.Name

			//}).OrderByDescending(x => x.CreatedDate);
			//return Ok(userDtos);
			var pagedlist = await _dailyReport.GetListDailyReport(dailyreportSearch);

			var menuDtos = pagedlist.Items.Select(x => new DailyReportView()
			{
				Id = x.Id,
				TodayTask = x.TodayTask,
				TomorrowTask = x.TomorrowTask,
				CreatedBy = x.AppUser.UserName,
				CreatedDate = x.CreatedDate,
				UpdatedDate = x.UpdatedDate,
				DepartmentName = x.Department.Name
			}).OrderByDescending(x => x.CreatedDate);

			return Ok(new PagedList<DailyReportView>(menuDtos.ToList(),
				pagedlist.MetaData.TotalCount,
				pagedlist.MetaData.CurrentPage,
				pagedlist.MetaData.PageSize));
		}
		[HttpGet("search")]
		public async Task<IActionResult> GetAllDailyRptSearch([FromQuery] DailyReportListSearch dailyrptsearch)
		{
			var rpt = await _dailyReport.GetListDailyRptSearch(dailyrptsearch);

			var rptDtos = rpt.Select(x => new DailyReportView()
			{
				Id = x.Id,
                TodayTask = x.TodayTask,
                TomorrowTask = x.TomorrowTask,
				CreatedBy = x.AppUser.UserName,
				CreatedDate = x.CreatedDate,
				UpdatedDate = x.UpdatedDate,
				DepartmentId = x.DepartmentId,
				DepartmentName = x.Department.Name,

                //HtmlBody = $"<h4 style='border-bottom: 1px solid #000; padding-bottom: 2px;'>{x.Department.Name}</h4>" +
                //   "<div style='display: flex;'>" +
                //   "<div style='flex: 1; padding: 10px;'>" +
                //   "<strong>금일</strong><br/>" +
                //   $"{x.TodayTask}" +
                //   "</div>" +
                //   "<div style='flex: 1; padding: 10px;'>" +
                //   "<strong>익일</strong><br/>" +
                //   $"{x.TomorrowTask}" +
                //   "</div>" +
                //   "</div>"

                //HtmlBody = $"<h4>{x.Department.Name}</h4>" +
                //       "<div style='padding: 1px;'>" +
                //       $"{ReplaceTags(x.TodayTask)}" +
                //       "</div>"

                HtmlBody = $"<h4 style='text-decoration: underline;font-size: 16px'>{x.Department.Name}</h4>" +
                       "<div style='padding: 1px;'>" +
                       $"{ReplaceTags(x.TodayTask)}" +
                       "</div>"

            }).OrderBy(x => x.DepartmentId);
			return Ok(rptDtos);
		}

        // Helper method to replace <ul> and <li> tags with <p> tags
        private string ReplaceTags(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            input = input.Replace("<ul>", "<p>");
            input = input.Replace("</ul>", "</p>");
            input = input.Replace("<li>", "<p>");
            input = input.Replace("</li>", "</p>");

            return input;
        }

        [HttpPost]
		public async Task<IActionResult> Create([FromBody] DailyReportCreateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _dailyReport.Create(new DailyReport()
			{
				TodayTask = request.TodayTask,
				TomorrowTask = request.TomorrowTask,
				CreatedDate = DateTime.Now,
				UserId = request.UserId,
				DepartmentId = request.DepartmentId
			});
			return Ok();
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _dailyReport.GetDailyReportById(id);
			if (result == null)
			{
				return NotFound($"{id} is not found");
			}

			return Ok(new DailyReportView()
			{
				TodayTask = result.TodayTask,
				TomorrowTask = result.TomorrowTask,
				DepartmentId = result.DepartmentId,
				UserId = result.UserId,
				Id = result.Id,
				CreatedDate = result.CreatedDate,
				UpdatedDate = result.UpdatedDate
			});
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] DailyReportUpdateRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var taskFromDb = await _dailyReport.GetDailyReportById(id);

			if (taskFromDb == null)
			{
				return NotFound($"{id} is not found");
			}

			taskFromDb.TodayTask = request.TodayTask;
			taskFromDb.TomorrowTask = request.TomorrowTask;
			taskFromDb.UpdatedDate = DateTime.Now;	
			taskFromDb.UserId = request.UserId;
			taskFromDb.DepartmentId = request.DepartmentId;
			var taskResult = await _dailyReport.Update(taskFromDb);

			return Ok();
		}
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var task = await _dailyReport.GetDailyReportById(id);
			if (task == null) return NotFound($"{id} is not found");

			await _dailyReport.Delete(task);
			return Ok();
		}
	}
}
