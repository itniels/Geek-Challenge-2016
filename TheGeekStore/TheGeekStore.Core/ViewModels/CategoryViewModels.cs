using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TheGeekStore.Core.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [Display(Name = "Category Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Category Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UploadedFile { get; set; }
    }

    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Category Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}
