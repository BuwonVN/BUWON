using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Asset
{
	public class AssetView
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string SerialNo { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string Location { get; set; }
		public int StatusId { get; set; }
		public int StatusName { get; set; }
		public string Description { get; set; }
		public double PurchasePrice { get; set; }
		public DateTime PurchaseDate { get; set; }
		public string AssignedTo { get; set; }
	}
}
