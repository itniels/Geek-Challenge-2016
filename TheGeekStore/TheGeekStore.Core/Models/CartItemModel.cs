using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    [Table(name: "CartItems")]
    public class CartItemModel
    {
        public int Id { get; set; }
        public ProductModel Product { get; set; }
        public CartModel Cart { get; set; }
        public int Count { get; set; }
    }
}
