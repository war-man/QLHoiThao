using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using QLHoiThaoProject.Models.EFModel;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class BANGDAUsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/BANGDAUs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.BANGDAUs.ToList().Count;
            return View(db.BANGDAUs.ToList().OrderBy(p => p.IDBANG).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.BANGDAUs.Where(p => p.TENBANG.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDBANG).ToList().ToPagedList(pageNumber, pageSize));
        }
        // GET: Admin/BANGDAUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANGDAU bANGDAU = db.BANGDAUs.Find(id);
            if (bANGDAU == null)
            {
                return HttpNotFound();
            }
            return View(bANGDAU);
        }

        // GET: Admin/BANGDAUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BANGDAUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDBANG,TENBANG")] BANGDAU bANGDAU)
        {
            if (ModelState.IsValid)
            {
                db.BANGDAUs.Add(bANGDAU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bANGDAU);
        }

        // GET: Admin/BANGDAUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANGDAU bANGDAU = db.BANGDAUs.Find(id);
            if (bANGDAU == null)
            {
                return HttpNotFound();
            }
            return View(bANGDAU);
        }

        // POST: Admin/BANGDAUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDBANG,TENBANG")] BANGDAU bANGDAU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bANGDAU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bANGDAU);
        }

        // GET: Admin/BANGDAUs/Delete/5
        public ActionResult Delete(int? id)
        {
            BANGDAU bANGDAU = db.BANGDAUs.Find(id);            
            return View(bANGDAU);
        }

        // POST: Admin/BANGDAUs/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            BANGDAU bANGDAU = db.BANGDAUs.Find(id);
            db.BANGDAUs.Remove(bANGDAU);
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
