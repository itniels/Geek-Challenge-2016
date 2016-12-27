using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.ViewModels
{
    public class CustomerProfileViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        public string PasswordCurrent { get; set; }

        public string Password1 { get; set; }

        public string Password2 { get; set; }
    }
}
