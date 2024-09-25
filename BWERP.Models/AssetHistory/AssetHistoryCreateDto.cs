using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.AssetHistory
{
	public class AssetHistoryCreateDto
	{
		public int Id { get; set; }	
		public string AssetId { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public string Event { get; set; }
	}
}
