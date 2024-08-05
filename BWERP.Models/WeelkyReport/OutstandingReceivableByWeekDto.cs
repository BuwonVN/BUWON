using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.WeelkyReport
{
	public class OutstandingReceivableByWeekDto
	{
		public string RowNum { get; set; }
		public string Department { get; set; } = string.Empty;
		public DateTime CreateDate { get; set; }
		public int EndAmount { get; set; }
	}
}
