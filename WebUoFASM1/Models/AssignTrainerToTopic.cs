﻿using System.ComponentModel.DataAnnotations;

namespace WebUoFASM1.Models
{
    public class AssignTrainerToTopic
    {
        [Key]
        public int Id { get; set; }

        public string TrainerId { get; set; }
        public int TopicId { get; set; }
        public ApplicationUser Trainer { get; set; }
        public Topic Topic { get; set; }
    }
}