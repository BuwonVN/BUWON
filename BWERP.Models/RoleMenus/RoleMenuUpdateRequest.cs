using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.RoleMenus
{
	public class RoleMenuUpdateRequest
	{
		[Required(ErrorMessage = "Please enter your task name")]
		public Guid RoleId { get; set; }

		[Required(ErrorMessage = "Please select your task priority")]
		public int MenuId { get; set; }
	}
}
