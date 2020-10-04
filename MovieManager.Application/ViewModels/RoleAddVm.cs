using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieManager.Application.ViewModels
{
    public class RoleAddVm
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}
