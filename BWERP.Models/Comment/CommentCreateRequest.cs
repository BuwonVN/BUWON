using BWERP.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Comment
{
	public class CommentCreateRequest
	{		
		[Required(ErrorMessage = "Please enter your content")]
		public string Content { get; set; } = string.Empty;

		//[Required(ErrorMessage = "Please select a function")]
		//public Functions Functions { get; set; }
		public int DepartmentId { get; set; }
		public Guid CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public Guid UpdatedBy { get; set; }
		public DateTime UpdatedDate { get; set; } = DateTime.Now;
	}
}
