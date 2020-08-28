using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebUoFASM1.Models;

namespace WebUoFASM1.Controllers
{
    public class TraineesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Trainees.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Trainees.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainee);
        }

        // GET: Trainees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trainee = db.Users.FirstOrDefault(p => p.Id == id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user)
        {
            var userInDb = db.Users.Find(user.Id);

            if (userInDb == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                userInDb.FullName = user.FullName;

                userInDb.PhoneNumber = user.PhoneNumber;
                userInDb.Email = user.Email;

                db.Users.AddOrUpdate(userInDb);
                db.SaveChanges();

                return RedirectToAction("Index", "Users");
            }
            return View(user);
        }

        // GET: Trainees/Delete/5
        public ActionResult Delete(ApplicationUser user)
        {
            var userInDb = db.Users.Find(user.Id);

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

                db.Users.Remove(userInDb);
                db.SaveChanges();

                return RedirectToAction("Index", "Users");
            }
            return View(user);
        }
    }
}