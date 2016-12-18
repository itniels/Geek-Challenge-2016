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

        public double Price { get; set; }

        public double PriceShipping { get; set; }

        // Cart and Items
        public virtual CartModel Cart { get; set; }

        // Address
        public string AdrName { get; set; }

        public string AdrAtt { get; set; }

        public string AdrStreet { get; set; }

        public int AdrPostal { get; set; }

        public string AdrCity { get; set; }

        public string AdrCountry { get; set; }


    }
}
