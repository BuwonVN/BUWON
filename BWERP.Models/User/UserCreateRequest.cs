using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.User
{
	public class UserCreateRequest
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }			
		public bool isActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastLogin { get; set; }
	}
}
