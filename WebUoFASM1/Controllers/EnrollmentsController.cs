using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUoFASM1.Models;
using WebUoFASM1.ViewModels;

namespace WebUoFASM1.Controllers
{
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext _context;

        public EnrollmentsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Staff"))
            {
                var traineecourses = _context.Enrollments.Include(t => t.Course).Include(t => t.Trainee).ToList();
                return View(traineecourses);
            }
            if (User.IsInRole("Trainee"))
            {
                var traineeId = User.Identity.GetUserId();
                var Res = _context.Enrollments.Where(e => e.TraineeId == traineeId).Include(t => t.Course).ToList();
                return View(Res);
            }
            return View("Login");
        }

        public ActionResult Assign()
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic

            var courses = _context.Courses.ToList();

            var TrainerTopicVM = new AssignTraineeToCourseViewModel()
            {
                Courses = courses,
                Trainees = users,
                Enrollment = new Enrollment()
            };

            return View(TrainerTopicVM);
        }

        [HttpPost]
        public ActionResult Assign(AssignTraineeToCourseViewModel model)
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic

            var courses = _context.Courses.ToList();

            if (ModelState.IsValid)
            {
                _context.Enrollments.Add(model.Enrollment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TrainerTopicVM = new AssignTraineeToCourseViewModel()
            {
                Courses = courses,
                Trainees = users,
                Enrollment = new Enrollment()
            };

            return View(TrainerTopicVM);
        }
    }
}