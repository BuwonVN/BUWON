﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Asset
{
	public class Asset
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int CategoryId { get; set; }
		public string Location { get; set; }
		public int StatusId { get; set; }
		public string Description { get; set; }
		public double PurChasePrice { get; set; }
		public DateTime PurchaseDate { get; set; }
		public string AssignedTo { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedUser { get; set; }
	}
}
