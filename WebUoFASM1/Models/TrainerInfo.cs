using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUoFASM1.Models
{
    public class TrainerInfo
    {
        [Key]
        public virtual ApplicationUser Trainer { get; set; }

        public string TrainerId { get; set; }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
        public string Type { get; set; }

        public string PhoneNumber { get; set; }
    }
}