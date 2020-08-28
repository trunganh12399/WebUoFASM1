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
    public class AssignTrainerToTopicsController : Controller
    {
        private ApplicationDbContext _context;

        public AssignTrainerToTopicsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Staff"))
            {
                var assignTrainerToTopics = _context.AssignTrainerToTopics.Include(t => t.Topic).Include(t => t.Trainer).ToList();
                return View(assignTrainerToTopics);
            }
            if (User.IsInRole("Trainer"))
            {
                var trainerId = User.Identity.GetUserId();
                var Res = _context.AssignTrainerToTopics.Where(e => e.TrainerId == trainerId).Include(t => t.Topic).ToList();
                return View(Res);
            }
            return View("Login");
        }

        public ActionResult Assign()
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic

            var topics = _context.Topics.ToList();

            var TrainerTopicVM = new AssignTrainerToTopicViewmodel()
            {
                Topics = topics,
                Trainers = users,
                AssignTrainerToTopic = new AssignTrainerToTopic()
            };

            return View(TrainerTopicVM);
        }

        [HttpPost]
        public ActionResult Assign(AssignTrainerToTopicViewmodel model)
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic

            var topics = _context.Topics.ToList();

            if (ModelState.IsValid)
            {
                _context.AssignTrainerToTopics.Add(model.AssignTrainerToTopic);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TrainerTopicVM = new AssignTrainerToTopicViewmodel()
            {
                Topics = topics,
                Trainers = users,
                AssignTrainerToTopic = new AssignTrainerToTopic()
            };

            return View(TrainerTopicVM);
        }
    }
}