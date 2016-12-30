using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TheGeekStore.Core.Models;

namespace TheGeekStore.Core.ViewModels
{
    public class ViewProductViewModel
    {
        public ProductModel Product { get; set; }
        public int PeopleReadyToBuy { get; set; }
        public IEnumerable<ProductModel> RelatedProducts { get; set; }
    }

    public class CreateProductViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Product Code")]
        public string ProductNumber { get; set; }

        [Display(Name = "Product Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Product Price")]
        public double Price { get; set; }

        [Display(Name = "In stock")]
        public int InStock { get; set; }

        public bool Featured { get; set; }

        [Display(Name = "Product Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UploadedFile { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Product Code")]
        public string ProductNumber { get; set; }

        [Display(Name = "Product Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Product Price")]
        public double Price { get; set; }

        [Display(Name = "In stock")]
        public int InStock { get; set; }

        public bool Featured { get; set; }

        [Display(Name = "Product Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}
