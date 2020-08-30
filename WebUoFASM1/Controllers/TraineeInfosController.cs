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
    public class TraineeInfosController : Controller
    {
        private ApplicationDbContext _context;

        public TraineeInfosController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize(Roles = "Staff, Trainee")]
        public ActionResult Index()
        {
            if (User.IsInRole("Staff"))
            {
                var trainees = _context.TraineeInfos.Include(t => t.Trainee).ToList();
                return View(trainees);
            }
            if (User.IsInRole("Trainee"))
            {
                var traineeId = User.Identity.GetUserId();
                var Res = _context.TraineeInfos.Where(e => e.TraineeId == traineeId).ToList();
                return View(Res);
            }
            return View("Login");
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Assign()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var TraineeVM = new TraineeViewModel()
            {
                Trainees = users,
                TraineeInfo = new TraineeInfo()
            };

            return View(TraineeVM);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public ActionResult Assign(TraineeViewModel traineeViewModel)
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            if (ModelState.IsValid)
            {
                _context.TraineeInfos.Add(traineeViewModel.TraineeInfo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TraineeVM = new TraineeViewModel()
            {
                Trainees = users,
                TraineeInfo = new TraineeInfo(),
            };

            return View(TraineeVM);
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Edit()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var TraineeVM = new TraineeViewModel()
            {
                Trainees = users,
                TraineeInfo = new TraineeInfo()
            };

            return View(TraineeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TraineeViewModel traineeViewModel)
        {
            var role = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var traineeInDb = _context.TraineeInfos.Find(traineeViewModel.TraineeInfo.Id);

            if (traineeInDb == null)
            {
                return View("Index");
            }

            if (ModelState.IsValid)
            {
                traineeInDb.Name = traineeViewModel.TraineeInfo.Name;
                traineeInDb.PhoneNumber = traineeViewModel.TraineeInfo.PhoneNumber;
                traineeInDb.Education = traineeViewModel.TraineeInfo.Education;
                traineeInDb.Email = traineeViewModel.TraineeInfo.Email;

                _context.TraineeInfos.AddOrUpdate(traineeInDb);
                _context.SaveChanges();

                return RedirectToAction("TraineeInfos");
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
            TraineeInfo traineeInfo = _context.TraineeInfos.Find(id);
            if (traineeInfo == null)
            {
                return HttpNotFound();
            }
            return View(traineeInfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TraineeInfo traineeInfo = _context.TraineeInfos.Find(id);
            _context.TraineeInfos.Remove(traineeInfo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}