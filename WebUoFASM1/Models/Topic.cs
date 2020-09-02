using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebUoFASM1.Models
{
    public class Topic
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Topic Name")]
        [Remote("IsTopicNameExist", "Course", AdditionalFields = "Id",
                ErrorMessage = "Course name already exists")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Topic Description")]
        public string Description { get; set; }
    }
}