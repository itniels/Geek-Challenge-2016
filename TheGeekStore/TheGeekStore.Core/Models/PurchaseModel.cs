using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        // Ship To
        // Name
        public string FullName { get; set; }

        // Email
        [EmailAddress]
        public string EMail { get; set; }

        // Phone
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        // Address
        public string AdrAtt { get; set; }

        public string AdrStreet1 { get; set; }

        public string AdrStreet2 { get; set; }

        [DataType(DataType.PostalCode)]
        public string AdrPostal { get; set; }

        public string AdrCity { get; set; }

        public string AdrState { get; set; }

        public string AdrCountry { get; set; }

        // Methods
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
