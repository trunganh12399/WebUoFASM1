using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebUoFASM1.Models;
using WebUoFASM1.ViewModels;

namespace WebUoFASM1.Controllers
{
    public class TraineeInfosController : Controller
    {
        private ApplicationDbContext _context;

        public TraineeInfosController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Staff"))
            {
                var traineecourses = _context.TraineeInfos.Include(t => t.Trainee).ToList();
                return View(traineecourses);
            }
            if (User.IsInRole("Trainee"))
            {
                var traineeId = User.Identity.GetUserId();
                var Res = _context.TraineeInfos.Where(e => e.TraineeId == traineeId).ToList();
                return View(Res);
            }
            return View("Login");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraineeInfo trainee = _context.TraineeInfos.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        public ActionResult Create()
        {
            //get trainee
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var TraineeTopicVM = new TraineeViewModel()
            {
                Trainees = users,
                TraineeInfo = new TraineeInfo()
            };

            return View(TraineeTopicVM);
        }

        [HttpPost]
        public ActionResult Create(TraineeViewModel traineeViewModel)
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                _context.TraineeInfos.Add(traineeViewModel.TraineeInfo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TraineeTopicVM = new TraineeViewModel()
            {
                Trainees = users,
                TraineeInfo = new TraineeInfo(),
            };

            return View(TraineeTopicVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user)
        {
            var userInDb = _context.Users.Find(user.Id);

            if (userInDb == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                userInDb.FullName = user.FullName;

                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.Email = user.Email;

                _context.Users.AddOrUpdate(userInDb);
                _context.SaveChanges();

                return RedirectToAction("Index", "Users");
            }
            return View(user);
        }

        // GET: Trainees/Delete/5
        public ActionResult Delete(ApplicationUser user)
        {
            var userInDb = _context.Users.Find(user.Id);

            if (userInDb == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                userInDb.UserName = user.UserName;
                userInDb.FullName = user.FullName;

                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.Email = user.Email;

                _context.Users.Remove(userInDb);
                _context.SaveChanges();

                return RedirectToAction("Index", "Users");
            }
            return View(user);
        }
    }
}