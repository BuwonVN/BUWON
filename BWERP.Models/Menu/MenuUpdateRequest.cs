using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Menu
{
    public class MenuUpdateRequest
    {      
        public string Name { get; set; }
        public string? Description { get; set; }

        public int? ParentId { get; set; }
        public string? Icon { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        //public IList<SelectListItem> SelectMenuItem { get; set; }
    }
}
