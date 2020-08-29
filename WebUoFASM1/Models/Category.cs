using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUoFASM1.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Category Description")]
        public string Description { get; set; }
    }
}