using System.Collections.Generic;
using WebUoFASM1.Models;

namespace WebUoFASM1.ViewModels
{
    public class TraineeViewModel
    {
        public TraineeInfo TraineeInfo { get; set; }

        public IEnumerable<ApplicationUser> Trainees { get; set; }
    }
}