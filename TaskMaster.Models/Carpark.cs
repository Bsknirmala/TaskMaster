using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskMaster.Models
{
    public class Carpark
    {
        [Key]
        public int Id { get; set; }

        [Display(Description = "Gantry Description")]
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public string Prefix { get; set; }
        public DateTime ts { get; set; }
        public DateTime df { get; set; }
        public Carpark()
        {
            ts = DateTime.Now;
            df = DateTime.MinValue;
        }
       
    }
}
