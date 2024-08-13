using System.ComponentModel.DataAnnotations;

namespace BWERP.Models.Exppense
{
	public class ExpenseCreateDto
	{
		public int Id { get; set; }
		public int DepartmentId { get; set; }

		[Required(ErrorMessage = "Please select Category")]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Please enter Description")]
		public string Description { get; set; }

		public DateTime CreatedDate { get; set; }
		public string CreatedUser { get; set; }

		[Required(ErrorMessage = "Please enter Amount")]
		public double Amount { get; set; }

		[Required(ErrorMessage = "Please enter Note")]
		public string Note { get; set; }
	}
}
