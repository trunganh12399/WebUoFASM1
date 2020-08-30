using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUoFASM1.Models;
using WebUoFASM1.ViewModels;

namespace WebUoFASM1.Controllers
{
    public class EditUserController : Controller
    {
        private ApplicationDbContext _context;

        public EditUserController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Index()
        {
            var manager = (from user in _context.Users
                           select new
                           {
                               UserId = user.Id,
                               Username = user.UserName,
                               UserPhone = user.PhoneNumber,
                               Emailaddress = user.Email,
                               Password = user.PasswordHash,
                               RoleNames = (from userRole in user.Roles
                                            join role in _context.Roles on userRole.RoleId
                                            equals role.Id
                                            select role.Name).ToList()
                           }).ToList().Select(p => new EditUserViewModel()

                           {
                               UserId = p.UserId,
                               Username = p.Username,
                               Email = p.Emailaddress,
                               Phone = p.UserPhone,
                               Role = string.Join(",", p.RoleNames)
                           });

            return View(manager);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
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
        [Authorize(Roles = "Admin, Staff")]
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
                userInDb.Email = user.Email;
                userInDb.FullName = user.FullName;
                userInDb.PhoneNumber = user.PhoneNumber;

                _context.Users.AddOrUpdate(userInDb);
                _context.SaveChanges();

                return RedirectToAction("Index", "EditUser");
            }
            return View(user);
        }

        [Authorize(Roles = "Admin,Staff")]
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
                userInDb.Email = user.Email;
                userInDb.FullName = user.FullName;
                userInDb.PhoneNumber = user.PhoneNumber;

                _context.Users.Remove(userInDb);
                _context.SaveChanges();

                return RedirectToAction("Index", "EditUser");
            }
            return View(user);
        }
    }
}