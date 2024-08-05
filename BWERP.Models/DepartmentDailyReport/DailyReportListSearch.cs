using BWERP.Models.SeedWork;

namespace BWERP.Models.DepartmentDailyReport
{
	public class DailyReportListSearch : PagingParameters
	{
		public string? UserId { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
