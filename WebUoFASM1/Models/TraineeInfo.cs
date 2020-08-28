using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUoFASM1.Models
{
    public class TraineeInfo
    {
        [Key]
        public virtual ApplicationUser Trainee { get; set; }

        public string TraineeId { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Education { get; set; }

        public string PhoneNumber { get; set; }
    }
}