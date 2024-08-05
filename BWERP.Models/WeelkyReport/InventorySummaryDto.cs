using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.WeelkyReport
{
	public class InventorySummaryDto
	{
		public int ItmsGrpCod { get; set; }
		public string ItmsGrpNam { get; set; }
		public double BeginQty { get; set; }
		public double EndQty { get; set; }
		public double InQty { get; set; }
		public double OutQty { get; set; }
	}
}
