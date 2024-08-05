using Microsoft.AspNetCore.Identity;

namespace BWERP.Api.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastLogin { get; set; }
		public bool isActive { get; set; }
		public Guid? RoleId { get; set; }
		public List<Task>? Tasks { get; set; }
        public List<DailyReport>? DailyReports { get; set; }

    }
}
