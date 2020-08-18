using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using PagedList;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class LOPsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/LOPs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.LOPs.Include(l => l.NGANH).ToList().Count;
            return View(db.LOPs.Include(l=>l.NGANH).ToList().OrderBy(p => p.IDLOP).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.LOPs.Include(l => l.NGANH).Where(p => p.TENLOP.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDLOP).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/LOPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP lOP = db.LOPs.Find(id);
            if (lOP == null)
            {
                return HttpNotFound();
            }
            return View(lOP);
        }

        // GET: Admin/LOPs/Create
        public ActionResult Create()
        {
            ViewBag.IDNGANH = new SelectList(db.NGANHs, "IDNGANH", "TENNGANH");
            return View();
        }

        // POST: Admin/LOPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLOP,IDNGANH,TENLOP")] LOP lOP)
        {
            if (ModelState.IsValid)
            {
                db.LOPs.Add(lOP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDNGANH = new SelectList(db.NGANHs, "IDNGANH", "TENNGANH", lOP.IDNGANH);
            return View(lOP);
        }

        // GET: Admin/LOPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP lOP = db.LOPs.Find(id);
            if (lOP == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNGANH = new SelectList(db.NGANHs, "IDNGANH", "TENNGANH", lOP.IDNGANH);
            return View(lOP);
        }

        // POST: Admin/LOPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLOP,IDNGANH,TENLOP")] LOP lOP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNGANH = new SelectList(db.NGANHs, "IDNGANH", "TENNGANH", lOP.IDNGANH);
            return View(lOP);
        }

        // GET: Admin/LOPs/Delete/5
        public ActionResult Delete(int id)
        {
            LOP lOP = db.LOPs.Find(id);
            return View(lOP);
        }

        // POST: Admin/LOPs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOP lOP = db.LOPs.Find(id);
            db.LOPs.Remove(lOP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
