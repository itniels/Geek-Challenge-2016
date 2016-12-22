using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    [Table(name: "Purchases")]
    public class PurchaseModel
    {
        public int Id { get; set; }

        // Purchase
        public string UserId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string PromoCode { get; set; }

        public double PriceShipping { get; set; }

        // Items purchased
        public virtual ICollection<PurchaseItemModel> PurchaseItems { get; set; }


        public double GetCartPrice()
        {
            double sum = 0;
            foreach (PurchaseItemModel item in PurchaseItems)
            {
                sum += item.GetTotalPrice();
            }
            return sum;
        }

        public double GetTotalPrice()
        {
            return GetCartPrice() + PriceShipping;
        }
    }
}
