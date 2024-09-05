using BWERP.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Asset
{
	public class AssetSearch : PagingParameters
	{
		public string? Name { get; set; }
		public int? CategoryId { get; set; }
		public string? Location { get; set; }
		public int? StatusId { get; set; }
	}
}
