﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.WeelkyReport
{
	public class OutstandingReceivableDto
	{
		public string Department { get; set; } =  string.Empty;
		public int Elapsdate { get; set; }
		public double BeginAmount { get; set; }
		public int IncreaseAmount { get; set; }
		public int DecreaseAmount { get; set; }
		public int EndAmount { get; set; }
	}
}
