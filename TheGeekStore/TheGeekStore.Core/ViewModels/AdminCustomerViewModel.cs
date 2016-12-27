using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGeekStore.Core.Models;

namespace TheGeekStore.Core.ViewModels
{
    public class AdminUserViewModel
    {
        public ApplicationUser User { get; set; }

        public CustomerProfileModel Address { get; set; }

        public ICollection<PurchaseModel> Orders { get; set; }
    }
}
