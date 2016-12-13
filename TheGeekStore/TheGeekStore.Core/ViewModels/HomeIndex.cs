using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGeekStore.Core.Models;

namespace TheGeekStore.Core.ViewModels
{
    public class HomeIndex
    {
        public IEnumerable<ProductModel> TopSold { get; set; }
        public IEnumerable<NewsModel> LatestNews { get; set; }
        public ProductModel Featuredproduct { get; set; }
    }
}
