using BWERP.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWERP.Models.Menu
{
    public class MenuCreateRequest
    {
        [MaxLength(100, ErrorMessage = "You cannot fill task name over than 100 characters")]
        [Required(ErrorMessage = "Please enter your task name")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? Icon { get; set; }
        public string? IconPath { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        //public IList<SelectListItem> SelectMenuItem { get; set; }
    }
}
