using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebUoFASM1.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [Remote("IsProductNameExist", "Category", AdditionalFields = "Id",
                ErrorMessage = "Product name already exists")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Category Description")]
        public string Description { get; set; }
    }
}