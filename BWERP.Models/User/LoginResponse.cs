﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.User
{
	public class LoginResponse
	{
		public bool Successful { get; set; }
		public string Error { get; set; }
		public string Token { get; set; }
	}
}
