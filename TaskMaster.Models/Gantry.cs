using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskMaster.Models
{
    public class Gantry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime ts { get; set; }
        public DateTime df { get; set; }
        public Gantry()
        {
            ts = DateTime.Now;
            df = DateTime.MinValue;
        }
        public int CarparkID { get; set; }
        [ForeignKey("CarparkID")]

        public Carpark Carpark { get; set; }

    }
}
