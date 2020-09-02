using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUoFASM1.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        public string TraineeId { get; set; }

        [Required]
        public int CourseId { get; set; }

        public ApplicationUser Trainee { get; set; }
        public Course Course { get; set; }
    }
}