using BWERP.Models.Enums;
using BWERP.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Comment
{
	public class CommentViewRequest : PagingParameters
	{
		public int Id { get; set; }
		public int DepartmentId { get; set; }
		public string FuncName { get; set; }
		public string Content { get; set; }
		public Guid CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedUser { get; set; }
		public Guid UpdatedBy { get; set; }
		public string UpdatedUser { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}
