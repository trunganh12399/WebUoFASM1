using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUoFASM1.Models;

namespace WebUoFASM1.ViewModels
{
    public class TraineeViewModel
    {
        public int TrainerInfo { get; set; }
        public TraineeInfo TraineeInfo { get; set; }

        public IEnumerable<ApplicationUser> Trainees { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Education { get; set; }

        public string PhoneNumber { get; set; }
    }
}