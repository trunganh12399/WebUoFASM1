using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUoFASM1.Models
{
    public class TraineeInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Education { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ApplicationUser Trainee { get; set; }

        public string TraineeId { get; set; }
    }
}