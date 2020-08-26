using System;
using System.Collections.Generic;
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
    }
}