using BWERP.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Task
{
	public class TaskViewRequest
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public Guid? AssigneeId { get; set; }
		public string Assignee { get; set; }
		public DateTime CreatedDate { get; set; }
		public Priority Priority { get; set; }
		public Status Status { get; set; }
	}
}
