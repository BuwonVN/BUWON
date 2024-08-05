using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.DepartmentDailyReport
{
	public class DailyReportCreateRequest
	{
		[Required(ErrorMessage = "Please enter Today Task")]
		public string TodayTask { get; set; } = string.Empty;
		[Required(ErrorMessage = "Please enter Tomorrow Task")]
		public string TomorrowTask { get; set; } = ".";
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public Guid UserId { get; set; }
		[Required(ErrorMessage = "Please select your department")]
		public int DepartmentId { get; set; }
	}
}
