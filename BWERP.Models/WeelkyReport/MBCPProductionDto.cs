using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.WeelkyReport
{
	public class MBCPProductionDto
	{
		public string CustomerName { get; set; }
		public int ProQty { get; set; }
		public int ProQty_M { get; set; }
		public int PlanQty_W { get; set; }
		public int PlanQty_M { get; set; }
		public double ProductionRate_W { get; set; }
		public double ProductionRate_M { get; set; }
	}
}
