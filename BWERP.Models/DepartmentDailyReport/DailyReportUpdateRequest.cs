using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.DepartmentDailyReport
{
	public class DailyReportUpdateRequest
	{
		public string TodayTask { get; set; }
		public string TomorrowTask { get; set; }
		public DateTime UpdatedDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public Guid UserId { get; set; }
		public int DepartmentId { get; set; }
	}
}
