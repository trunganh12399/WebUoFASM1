using System.Collections.Generic;
using WebUoFASM1.Models;

namespace WebUoFASM1.ViewModels
{
    public class AssignTraineeToCourseViewModel
    {
        public Enrollment Enrollment { get; set; }
        public IEnumerable<ApplicationUser> Trainees { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}