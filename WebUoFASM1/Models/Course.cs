using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebUoFASM1.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [DisplayName("Course Name")]
        [Remote("IsCourseNameExist", "Course", AdditionalFields = "Id",
                ErrorMessage = "Course name already exists")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Course Description")]
        public string Description { get; set; }
    }
}