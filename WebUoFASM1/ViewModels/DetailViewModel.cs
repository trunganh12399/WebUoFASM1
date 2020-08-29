using System.Collections.Generic;
using WebUoFASM1.Models;

namespace WebUoFASM1.ViewModels
{
    public class DetailViewModel
    {
        public Detail Detail { get; set; }
        public IEnumerable<ApplicationUser> Trainers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
    }
}