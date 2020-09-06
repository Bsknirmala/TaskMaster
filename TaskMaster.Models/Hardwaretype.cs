using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskMaster.Models
{
    public class Hardwaretype
    {
        [Key]
        public int Id { get; set; }

        [Display(Description = "Hardware Description")]
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime ts { get; set; }
        public DateTime df { get; set; }
        public Hardwaretype()
        {
            ts = DateTime.Now;
            df = DateTime.MinValue;
        }
       
    }
}
