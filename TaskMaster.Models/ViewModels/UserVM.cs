using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskMaster.Models.ViewModels
{
    public class UserVM
    {
        public User User { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [MaxLength(8)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
