using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.DepartmentDailyReport
{
	public class DailyReportView
	{
		public int Id { get; set; }
		public string? TodayTask { get; set; }
		public string? TomorrowTask { get; set; }
		public DateTime CreatedDate { get; set; }		
		public DateTime UpdatedDate { get; set; }
		public Guid UserId { get; set; }
		public string CreatedBy { get; set; }
		public int DepartmentId { get; set; }
		public string DepartmentName { get; set; }
		public string HtmlBody { get; set; }
	}
}
