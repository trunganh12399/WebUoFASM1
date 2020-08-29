using System.ComponentModel.DataAnnotations;

namespace WebUoFASM1.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        public string TraineeId { get; set; }
        public int CourseId { get; set; }
        public ApplicationUser Trainee { get; set; }
        public Course Course { get; set; }
    }
}