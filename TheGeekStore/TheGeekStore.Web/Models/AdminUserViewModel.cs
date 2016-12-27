using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGeekStore.Core.Models;
using TheGeekStore.Models;

namespace TheGeekStore.Models
{
    public class AdminUserViewModel
    {
        public ApplicationUser User { get; set; }

        public CustomerProfileModel Address { get; set; }

        public ICollection<PurchaseModel> Orders { get; set; }
    }
}
