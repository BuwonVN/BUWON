using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.WeelkyReport
{
	public class IPOSProductionDto
	{
		public string Dept { get; set; }
		public int PlanQty { get; set; }
		public int PlanQty_M { get; set; }

		public int ProQty { get; set; }
		public int ProQty_M { get; set; }

		public double ProductionRate { get; set; }
		public double ProductionRate_M { get; set; }

		public int DefectQty { get; set; }
		public int DefectQty_M { get; set; }

		public double DefectRate { get; set; }
		public double DefectRate_M { get; set; }
	}
}
