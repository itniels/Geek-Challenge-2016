using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.JsonModels
{
    public class JsonCartAdjustAmount
    {
        // Product
        public int Count { get; set; }
        public double TotalPrice { get; set; }

        // Cart
        public double CartPrice { get; set; }
    }
}
