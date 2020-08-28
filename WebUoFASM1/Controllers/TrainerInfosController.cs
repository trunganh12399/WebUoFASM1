using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebUoFASM1.Migrations;
using WebUoFASM1.Models;
using WebUoFASM1.ViewModels;

namespace WebUoFASM1.Controllers
{
    public class TrainerInfosController : Controller
    {
        private ApplicationDbContext _context;

        public TrainerInfosController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Staff"))
            {
                var traineecourses = _context.TrainerInfos.Include(t => t.Trainer).ToList();
                return View(traineecourses);
            }
            if (User.IsInRole("Trainer"))
            {
                var trainerId = User.Identity.GetUserId();
                var Res = _context.TrainerInfos.Where(e => e.TrainerId == trainerId).ToList();
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
            TrainerInfo trainer = _context.TrainerInfos.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        public ActionResult Create()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var TraineeTopicVM = new TrainerViewModel()
            {
                Trainers = users,
                TrainerInfo = new TrainerInfo()
            };

            return View(TraineeTopicVM);
        }

        [HttpPost]
        public ActionResult Create(TrainerViewModel trainerViewModel)
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                _context.TrainerInfos.Add(trainerViewModel.TrainerInfo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TraineeTopicVM = new TrainerViewModel()
            {
                Trainers = users,
                TrainerInfo = new TrainerInfo(),
            };

            return View(TraineeTopicVM);
        }
    }
}