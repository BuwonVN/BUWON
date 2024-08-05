using BWERP.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.User
{
	public class UserListSearch : PagingParameters
	{
		public string? Username { get; set; }
		public string? Email { get; set; }
	}
}
