using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    [Table(name: "Carts")]
    public class CartModel
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }

        public DateTime LastAccessed { get; set; }

        public virtual ICollection<CartItemModel> CartItems { get; set; }

        public double GetTotalPrice()
        {
            double total = 0;
            if (CartItems != null)
            {
                foreach (CartItemModel item in CartItems)
                {
                    if (item.Product != null)
                    {
                        total += (item.Product.Price*item.Count);
                    }
                }
            }
            return total;
        }

        public int GetTotalItemCount()
        {
            int count = 0;
            if (CartItems != null)
            {
                foreach (CartItemModel item in CartItems)
                {
                    count += item.Count;
                }
            }
            return count;
        }
    }
}
