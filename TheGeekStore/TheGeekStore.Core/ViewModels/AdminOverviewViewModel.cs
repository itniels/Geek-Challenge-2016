using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGeekStore.Core.Models;

namespace TheGeekStore.Core.ViewModels
{
    public class AdminOverviewViewModel
    {
        public int CountCategories { get; set; }
        public int CountProducts { get; set; }
        public int CountUsers { get; set; }
        public int CountAdmins { get; set; }
        public int CountFeaturedProducts { get; set; }
        public int CountPurchases { get; set; }

    }
}
