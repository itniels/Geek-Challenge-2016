using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    [Table(name:"DailyDeals")]
    public class DailyDealModel
    {
        public int Id { get; set; }

        public ProductModel Product { get; set; }

        public double ProductOriginalPrice { get; set; }
    }
}
