using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
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

        [Authorize(Roles = "Staff, Trainee")]
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

        [Authorize(Roles = "Staff")]
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

            var TraineeTopicVM = new AssignTraineeToCourseViewModel()
            {
                Courses = courses,
                Trainees = users,
                Enrollment = new Enrollment()
            };

            return View(TraineeTopicVM);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Edit()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssignTraineeToCourseViewModel model)
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var courses = _context.Courses.ToList();

            if (ModelState.IsValid)
            {
                _context.Enrollments.AddOrUpdate(model.Enrollment);
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

        [Authorize(Roles = "Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Enrollment enrollment = _context.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = _context.Enrollments.Find(id);
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}