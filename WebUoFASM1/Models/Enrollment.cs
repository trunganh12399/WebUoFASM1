using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUoFASM1.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Course Name")]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        [DisplayName("Trainee Name")]
        public int TraineeId { get; set; }

        public Trainee Trainee { get; set; }
    }
}