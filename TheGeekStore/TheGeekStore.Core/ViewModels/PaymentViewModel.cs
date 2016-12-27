using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGeekStore.Core.Enums;

namespace TheGeekStore.Core.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        public string Month { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public string CVV { get; set; }

        public double PaymentAmount { get; set; }

    }
}
