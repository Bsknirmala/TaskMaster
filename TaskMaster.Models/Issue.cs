using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskMaster.Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string  Reportedby { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="0:yyyy-MM-dd HH:mm:ss}")]
        [Required]
        public DateTime Issuedt { get; set; }
        [Required]
        public string Action { get; set; }

        public string ServiceRptno { get; set; }
        [Required]
        public string Issuestatus { get; set; }

        public DateTime ts { get; set; }
        public DateTime df { get; set; }
        public Issue()
        {
            Issuedt = DateTime.Now;
            ts = DateTime.Now;
            df = DateTime.MinValue;
        }
        public int CarparkID { get; set; }
        [ForeignKey("CarparkID")]
        public Carpark Carpark { get; set; }

        public int GantryID { get; set; }
        public Gantry Gantry { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey(" CategoryID")]
        public Hardwaretype Hardwaretype { get; set; }

        public enum StatusList
            { 
                TechFollowup,
                OpsFollowup,
                Closed
            }
    }
    
}
