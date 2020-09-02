using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebUoFASM1.Models;

namespace WebUoFASM1.Controllers
{
    [Authorize(Roles = "Staff")]
    public class TopicsController : Controller

    {
        private ApplicationDbContext _context;

        public TopicsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var list = _context.Topics.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic model)
        {
            bool IsTopicNameExist = _context.Topics.Any
                 (x => x.Name == model.Name && x.Id != model.Id);
            if (IsTopicNameExist == true)
            {
                ModelState.AddModelError("Name", "Topic Name already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Topics.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public JsonResult IsProductNameExist(string TopicName, int? Id)
        {
            var validateName = _context.Topics.FirstOrDefault
                                (x => x.Name == TopicName && x.Id != Id);
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Topic topic = _context.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name, Description")]
                          Topic model)
        {
            bool IsTopicNameExist = _context.Topics.Any
                            (x => x.Name == model.Name && x.Id != model.Id);
            if (IsTopicNameExist == true)
            {
                ModelState.AddModelError("Name", "Topic Name already exists");
            }

            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var topicInDb = _context.Topics.SingleOrDefault(p => p.Id == id);

            if (topicInDb == null)
            {
                return HttpNotFound();
            }

            _context.Topics.Remove(topicInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}