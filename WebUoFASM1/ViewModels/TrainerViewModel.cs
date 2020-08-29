using System.Collections.Generic;
using WebUoFASM1.Models;

namespace WebUoFASM1.ViewModels
{
    public class TrainerViewModel
    {
        public TrainerInfo TrainerInfo { get; set; }

        public IEnumerable<ApplicationUser> Trainers { get; set; }
    }
}