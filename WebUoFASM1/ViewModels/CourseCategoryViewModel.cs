using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUoFASM1.Models;

namespace WebUoFASM1.ViewModels
{
    public class CourseCategoryViewModel
    {
        public Course Course { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}