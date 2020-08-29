using System.ComponentModel.DataAnnotations;

namespace WebUoFASM1.Models
{
    public class TrainerInfo
    {
        [Key]
        public virtual ApplicationUser Trainer { get; set; }

        public string TrainerId { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }

        public string PhoneNumber { get; set; }
    }
}