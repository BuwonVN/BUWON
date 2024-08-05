using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.WeelkyReport
{
	public class DateRangeSearch
	{
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public int Year { get; set; } 
		public int Month { get; set; } 
	}
}
