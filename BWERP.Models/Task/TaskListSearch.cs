using BWERP.Models.Enums;
using BWERP.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Task
{
	public class TaskListSearch : PagingParameters
	{
		public string? Name { get; set; }

		public Guid? AssigneeId { get; set; }

		public Priority? Priority { get; set; }
	}
}
