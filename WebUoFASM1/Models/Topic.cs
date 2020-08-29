using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUoFASM1.Models
{
    public class Topic
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Topic Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Topic Description")]
        public string Description { get; set; }
    }
}