using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebUoFASM1.Models;
using WebUoFASM1.ViewModels;

namespace WebUoFASM1.Controllers
{
    public class DetailsController : Controller
    {
        private ApplicationDbContext _context;

        public DetailsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Staff"))
            {
                var details = _context.Details.Include(t => t.Course).Include(t => t.Topic).Include(t => t.Trainer).ToList();
                return View(details);
            }
            if (User.IsInRole("Trainer"))
            {
                var trainerId = User.Identity.GetUserId();
                var Res = _context.Details.Where(e => e.TrainerId == trainerId).Include(t => t.Course).Include(t => t.Topic).ToList();
                return View(Res);
            }
            return View("Login");
        }

        public ActionResult Assign()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var courses = _context.Courses.ToList();
            var topics = _context.Topics.ToList();

            var TrainerTopicVM = new DetailViewModel()
            {
                Courses = courses,
                Topics = topics,
                Trainers = users,
                Detail = new Detail()
            };

            return View(TrainerTopicVM);
        }

        [HttpPost]
        public ActionResult Assign(DetailViewModel model)
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var courses = _context.Courses.ToList();
            var topics = _context.Topics.ToList();

            if (ModelState.IsValid)
            {
                _context.Details.Add(model.Detail);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TrainerTopicVM = new DetailViewModel()
            {
                Courses = courses,
                Topics = topics,
                Trainers = users,
                Detail = new Detail()
            };

            return View(TrainerTopicVM);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Detail detail = _context.Details.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Name", detail.CourseId);
            ViewBag.TopicId = new SelectList(_context.Topics, "Id", "Name", detail.TopicId);
            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,TopicId")] Detail detail)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(detail).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Name", detail.CourseId);
            ViewBag.TopicId = new SelectList(_context.Topics, "Id", "Name", detail.TopicId);
            return View(detail);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Detail detail = _context.Details.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detail detail = _context.Details.Find(id);
            _context.Details.Remove(detail);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}