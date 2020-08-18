using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using PagedList;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class NGANHsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/NGANHs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.NGANHs.ToList().Count;
            return View(db.NGANHs.ToList().OrderBy(p => p.IDNGANH).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.NGANHs.Where(p => p.TENNGANH.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDNGANH).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/NGANHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGANH nGANH = db.NGANHs.Find(id);
            if (nGANH == null)
            {
                return HttpNotFound();
            }
            return View(nGANH);
        }

        // GET: Admin/NGANHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NGANHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNGANH,TENNGANH")] NGANH nGANH)
        {
            if (ModelState.IsValid)
            {
                db.NGANHs.Add(nGANH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nGANH);
        }

        // GET: Admin/NGANHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGANH nGANH = db.NGANHs.Find(id);
            if (nGANH == null)
            {
                return HttpNotFound();
            }
            return View(nGANH);
        }

        // POST: Admin/NGANHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNGANH,TENNGANH")] NGANH nGANH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGANH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nGANH);
        }

        // GET: Admin/NGANHs/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            NGANH nganh = db.NGANHs.Find(id);
            return RedirectToAction("Index");
        }

        // POST: Admin/NGANHs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NGANH nGANH = db.NGANHs.Find(id);
            db.NGANHs.Remove(nGANH);
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
