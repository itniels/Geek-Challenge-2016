using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [CreditCard]
        public int CardNumber { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        [Range(2012, 2030)]
        public int Year { get; set; }

        [Required]
        [Range(000,999)]
        public int CVV { get; set; }
    }
}
