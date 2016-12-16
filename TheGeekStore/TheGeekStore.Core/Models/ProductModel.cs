using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGeekStore.Core.Models
{
    [Table("Products")]
    public class ProductModel
    {
        public int Id { get; set; }

        public string ProductNumber { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public string ImagePath { get; set; }
        [Required]
        public double Price { get; set; }

        public virtual CategoryModel Category { get; set; }

        public int TimesPuchased { get; set; }

        public int InStock { get; set; }

        public bool Featured { get; set; }

        public string GetExcerpt()
        {
            int cutoff = 130;
            if (Description.Length > cutoff)
            {
                var excerpt = Description.Substring(0, cutoff);
                excerpt += "...";
                return excerpt;
            }
            return Description;
        }
    }
}
