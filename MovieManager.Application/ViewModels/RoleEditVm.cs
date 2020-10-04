using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieManager.Application.ViewModels
{
    public class RoleEditVm
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        public string Name { get; set; }

        public List<string> Users;

        public RoleEditVm()
        {
            Users = new List<string>();
        }
    }
}
