using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.WeelkyReport
{
	public class IPOSMonthlyProdDto
	{
		public int ProdYear { get; set; }
		public string ProdMonth { get; set; }
		public int ProQty { get; set; }
		public int DefectQty { get; set; }
	}
}
