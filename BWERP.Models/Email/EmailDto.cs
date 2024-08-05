using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Email
{
	public class EmailDto
	{
		public string ToAdress { get;set; } = string.Empty;
		public string CcAddress { get; set; } = string.Empty;

		public string Subject { get;set; } = string.Empty;
		public string Body { get;set; } = string.Empty;
	}
}
