namespace BWERP.Models.User
{
	public class UserViewRequest
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime lastLogin { get; set; }
		public bool isActive { get; set; }
        public Guid? RoleId { get; set; }
    }
}
