using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUoFASM1.Models;

namespace WebUoFASM1.ViewModels
{
    public class TrainerViewModel
    {
        public TrainerInfo TrainerInfo { get; set; }

        public IEnumerable<ApplicationUser> Trainers { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }

        public string PhoneNumber { get; set; }
    }
}