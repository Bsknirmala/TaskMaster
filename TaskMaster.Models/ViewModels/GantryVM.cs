using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMaster.Models.ViewModels
{
    public class GantryVM
    {
        public Gantry Gantry { get; set; }
        public IEnumerable<SelectListItem> CarparkList { get; set; }
        public int CarparkID { get; set; }
    }
}
