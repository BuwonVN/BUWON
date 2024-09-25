using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Asset
{
	public class AssetUpdateDto
	{
		public string Id { get; set; }

		[Required(ErrorMessage = "Please enter Asset Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter Serial No")]
		public string SerialNo { get; set; }

		public string? Location { get; set; }
		public int StatusId { get; set; }

		public string? Description { get; set; }

		[Required(ErrorMessage = "Please enter Amount")]
		public double PurchasePrice { get; set; }

		public DateTime PurchaseDate { get; set; }
		public string? AssignedTo { get; set; }
	}
}
