using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUoFASM1.Models
{
    public class Detail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Course Name")]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        [DisplayName("Topic Name")]
        public int TopicId { get; set; }

        public Topic Topic { get; set; }
        public ApplicationUser Trainer { get; set; }
        public string TrainerId { get; set; }
    }
}