using BWERP.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Exppense
{
    public class ExpenseSearch : PagingParameters
    {
		public int Year { get; set; }
		public int Month { get; set; }
		public int? CategoryId { get; set; }
	}
}
