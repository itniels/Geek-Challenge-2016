using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    [Table(name: "PurchaseItems")]
    public class PurchaseItemModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public double GetTotalPrice()
        {
            return (Price * Count);
        }
    }
}
