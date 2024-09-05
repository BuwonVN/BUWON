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
		public string Name { get; set; }
		[Required(ErrorMessage = "Please select Category")]
		public int CategoryId { get; set; }

		public string? Location { get; set; }
		public int StatusId { get; set; }

		[Required(ErrorMessage = "Please enter Description")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please enter Amount")]
		public double PurChasePrice { get; set; }
		public DateTime PurChaseDate { get; set; }

		public string? AssignedTo { get; set; }
	}
}
