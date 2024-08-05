using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.RoleMenus
{
	public class RoleMenusDto
	{
		public int Id { get; set; }	
		public Guid RoleId { get; set; }
		public string RoleName { get; set; } = string.Empty;
		public int MenuId { get; set; }
		public string MenuName { get; set; } = string.Empty;

	}
}
