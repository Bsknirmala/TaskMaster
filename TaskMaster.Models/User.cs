using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskMaster.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(8)]
        public string Password { get; set; }
       
    
        [Required]
        public string AccessLevel { get; set; }
        [Required]
        public string Dept { get; set; }

        public DateTime ts { get; set; }
        public DateTime df { get; set; }
        public User()
        {
            ts = DateTime.Now;
            df = DateTime.MinValue;
        }

        public enum DeptList

        {
            Technical,
            Operation,
            CentralControl,
            Others
        }
        public enum AccessList

        {
            Admin,
            Manager,
            User
        }
    }
}
