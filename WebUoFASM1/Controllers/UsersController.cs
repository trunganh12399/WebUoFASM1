using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebUoFASM1.Models;

namespace WebUoFASM1.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Index()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Staff") select r).FirstOrDefault();
            var staffs = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var Staffs = staffs.Select(user => new UserViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                RoleName = "Staff"
            }).ToList();

            var role1 = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
            var trainers = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role1.Id)).ToList();

            var Trainers = trainers.Select(user => new UserViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                RoleName = "Trainer"
            }).ToList();

            var role3 = (from r in _context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var trainees = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role3.Id)).ToList();

            var Trainees = trainees.Select(user => new UserViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                RoleName = "Trainee"
            }).ToList();

            var model = new GroupedUserViewModel { Staffs = Staffs, Trainees = Trainees, Trainers = Trainers, };
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var appUser = _context.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ApplicationUser user)
        {
            var userInDb = _context.Users.Find(user.Id);

            if (userInDb == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                userInDb.UserName = user.UserName;
                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.Email = user.Email;

                _context.Users.AddOrUpdate(userInDb);
                _context.SaveChanges();

                return RedirectToAction("UserPage");
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
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
                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.Email = user.Email;

                _context.Users.Remove(userInDb);
                _context.SaveChanges();

                return RedirectToAction("UserPage");
            }
            return View(user);
        }
    }
}