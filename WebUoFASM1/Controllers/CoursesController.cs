using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebUoFASM1.Models;
using WebUoFASM1.ViewModels;

namespace WebUoFASM1.Controllers
{
    [Authorize(Roles = "Staff")]
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Products
        [HttpGet]
        public ActionResult Index()
        {
            var list = _context.Courses.Include(p => p.Category).ToList();
            return View(list);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseCategoryViewModel();
            viewModel.Categories = _context.Categories.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            bool IsCourseNameExist = _context.Courses.Any
                   (x => x.Name == course.Name && x.Id != course.Id);
            if (IsCourseNameExist == true)
            {
                ModelState.AddModelError("Name", "Course Name already exists");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Staff")]
        public ActionResult Delete(int id)
        {
            var productInDb = _context.Courses.SingleOrDefault(p => p.Id == id);

            if (productInDb == null)
            {
                return HttpNotFound();
            }

            _context.Courses.Remove(productInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var CourseInDb = _context.Courses.SingleOrDefault(m => m.Id == id);
            if (CourseInDb == null) return HttpNotFound();

            var viewModel = new CourseCategoryViewModel()
            {
                Categories = _context.Categories.ToList(),
                Course = CourseInDb
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public ActionResult Edit(Course course)
        {
            bool IsCourseNameExist = _context.Courses.Any
                           (x => x.Name == course.Name && x.Id != course.Id);
            if (IsCourseNameExist == true)
            {
                ModelState.AddModelError("Name", "Course Name already exists");
            }
            var CourseInDb = _context.Courses.SingleOrDefault(m => m.Id == course.Id);
            CourseInDb.Name = course.Name;
            CourseInDb.Description = course.Description;
            CourseInDb.CategoryId = course.CategoryId;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult IsCourseNameExist(string ProductName, int? Id)
        {
            var validateName = _context.Courses.FirstOrDefault
                                (x => x.Name == ProductName && x.Id != Id);
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}