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
        public string Id { get; set; }
        
        public string UserId { get; set; }

        public virtual ICollection<CartItemModel> CartItems { get; set; }
    }
}
