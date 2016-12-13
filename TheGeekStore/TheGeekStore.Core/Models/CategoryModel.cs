using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    [Table("Categories")]
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<ProductModel> Products { get; set; }
    }
}
