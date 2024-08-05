using BWERP.Models.Enums;

namespace BWERP.Api.Entities
{
	public class Task
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public Guid? AssigneeId { get; set; }
		public AppUser? Assignee { get; set; }
		public DateTime CreatedDate { get; set; }
		public Priority Priority { get; set; }
		public Status Status { get; set; }
	}
}
