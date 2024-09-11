using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Asset
{
	public class AssetCreateDto
	{
		public string Id { get; set; }
		[Required(ErrorMessage = "Please enter Description")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please select Category")]
		public int CategoryId { get; set; }

		public string? Location { get; set; }

		[Required(ErrorMessage = "Please select Category")]
		public int StatusId { get; set; }

		public string? Description { get; set; }

		[Required(ErrorMessage = "Please enter Amount")]
		public double PurChasePrice { get; set; }

		public DateTime PurchaseDate { get; set; }
		public string? AssignedTo { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedUser { get; set; }
	}
}
