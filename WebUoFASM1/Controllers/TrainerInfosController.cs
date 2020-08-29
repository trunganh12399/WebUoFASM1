using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
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

        [Authorize(Roles = "Staff, Trainer")]
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

        [Authorize(Roles = "Staff")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerInfo trainerInfo = _context.TrainerInfos.Find(id);
            if (trainerInfo == null)
            {
                return HttpNotFound();
            }
            return View(trainerInfo);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Assign()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var TrainerTopicVM = new TrainerViewModel()
            {
                Trainers = users,
                TrainerInfo = new TrainerInfo()
            };

            return View(TrainerTopicVM);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public ActionResult Assign(TrainerViewModel trainerViewModel)
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                _context.TrainerInfos.Add(trainerViewModel.TrainerInfo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TrainerTopicVM = new TrainerViewModel()
            {
                Trainers = users,
                TrainerInfo = new TrainerInfo(),
            };

            return View(TrainerTopicVM);
        }

        [Authorize(Roles = "Staff, Trainer")]
        public ActionResult Edit()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var TrainerTopicVM = new TrainerViewModel()
            {
                Trainers = users,
                TrainerInfo = new TrainerInfo()
            };

            return View(TrainerTopicVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainerViewModel trainerViewModel)
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var trainerInDb = _context.TrainerInfos.Find(trainerViewModel.TrainerInfo);

            if (trainerInDb == null)
            {
                return View("Index");
            }

            if (ModelState.IsValid)
            {
                trainerInDb.Name = trainerViewModel.TrainerInfo.Name;
                trainerInDb.PhoneNumber = trainerViewModel.TrainerInfo.PhoneNumber;
                trainerInDb.Email = trainerViewModel.TrainerInfo.Email;
                trainerInDb.Type = trainerViewModel.TrainerInfo.Type;

                _context.TrainerInfos.AddOrUpdate(trainerInDb);
                _context.SaveChanges();

                return RedirectToAction("UsersWithRoles");
            }
            return View("Index");
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerInfo trainerInfo = _context.TrainerInfos.Find(id);
            if (trainerInfo == null)
            {
                return HttpNotFound();
            }
            return View(trainerInfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainerInfo trainerInfo = _context.TrainerInfos.Find(id);
            _context.TrainerInfos.Remove(trainerInfo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}