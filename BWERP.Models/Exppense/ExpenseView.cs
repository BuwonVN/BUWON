using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Exppense
{
    public class ExpenseView
    {
		public int Id { get; set; }
		public int DepartmentId { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedUser { get; set; }
		public double Amount { get; set; }
		public string Note { get; set; }
	}
}
