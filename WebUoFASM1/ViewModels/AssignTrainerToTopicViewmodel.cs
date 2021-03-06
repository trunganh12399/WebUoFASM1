﻿using System.Collections.Generic;
using WebUoFASM1.Models;

namespace WebUoFASM1.ViewModels
{
    public class AssignTrainerToTopicViewmodel
    {
        public AssignTrainerToTopic AssignTrainerToTopic { get; set; }
        public IEnumerable<ApplicationUser> Trainers { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
    }
}