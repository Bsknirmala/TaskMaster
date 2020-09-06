using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskMaster.Models.ViewModels
{
    public class IssueVM
    {
        public Issue Issue { get; set; }
        public IEnumerable<SelectListItem> CarparkList { get; set; }

        public IEnumerable<SelectListItem> GantryList { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public int CarparkID { get; set; }
        public int GantryID { get; set; }

        public Gantry Gantry { get; set; }
        public int CategoryID { get; set; }
    }
}
