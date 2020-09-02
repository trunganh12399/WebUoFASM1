using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUoFASM1.Models
{
    public class AssignTrainerToTopic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TrainerId { get; set; }

        [Required]
        public int TopicId { get; set; }

        public ApplicationUser Trainer { get; set; }
        public Topic Topic { get; set; }
    }
}