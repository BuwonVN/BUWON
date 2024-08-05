using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.User
{
	public class UserUpdateRequest
	{
		public string Username { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string Email { get; set; }
		public bool isActive { get; set; }
		public DateTime LastLogin { get; set; }
	}
}
