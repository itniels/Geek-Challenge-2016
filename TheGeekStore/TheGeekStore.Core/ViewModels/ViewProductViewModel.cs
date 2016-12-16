using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGeekStore.Core.Models;

namespace TheGeekStore.Core.ViewModels
{
    public class ViewProductViewModel
    {
        public ProductModel Product { get; set; }
        public IEnumerable<ProductModel> RelatedProducts { get; set; }
    }
}
