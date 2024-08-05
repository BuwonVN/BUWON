using BWERP.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.RoleMenus
{
	public class RoleMenuListSearch : PagingParameters
	{
		public Guid RoleId { get; set; }
	}
}
