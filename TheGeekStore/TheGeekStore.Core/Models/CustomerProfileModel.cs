using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    public class CustomerProfileModel
    {
        // Customer
        public int Id { get; set; }

        public string UserId { get; set; }

        // Name
        public string FullName { get; set; }

        // Phone
        public string PhoneNumber { get; set; }

        // Address
        public string AdrAtt { get; set; }

        public string AdrStreet1 { get; set; }

        public string AdrStreet2 { get; set; }

        public int AdrPostal { get; set; }

        public string AdrCity { get; set; }

        public string AdrState { get; set; }

        public string AdrCountry { get; set; }
    }
}
