using BWERP.Models.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Menu
{
    public class MenuListSearch : PagingParameters
    {
        public string? Name { get; set; }
    }
}
