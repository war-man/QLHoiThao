using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using PagedList;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class MONTHIDAUsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/MONTHIDAUs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.MONTHIDAUs.ToList().Count;
            return View(db.MONTHIDAUs.ToList().OrderBy(p=>p.IDMTD).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.MONTHIDAUs.Where(p => p.TENMTD.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDMTD).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/MONTHIDAUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONTHIDAU mONTHIDAU = db.MONTHIDAUs.Find(id);
            if (mONTHIDAU == null)
            {
                return HttpNotFound();
            }
            return View(mONTHIDAU);
        }

        // GET: Admin/MONTHIDAUs/Create
        public ActionResult Create()
        {
            ViewBag.IDLOAIMTD = new SelectList(db.LOAIMTDs, "IDLOAIMTD", "TENLOAIMTD");
            return View();
        }

        // POST: Admin/MONTHIDAUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMTD,IDLOAIMTD,TENMTD,MOTA,HINHANH")] MONTHIDAU mONTHIDAU)
        {
            if (ModelState.IsValid)
            {
                db.MONTHIDAUs.Add(mONTHIDAU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLOAIMTD = new SelectList(db.LOAIMTDs, "IDLOAIMTD", "TENLOAIMTD", mONTHIDAU.IDMTD);
            return View(mONTHIDAU);
        }

        // GET: Admin/MONTHIDAUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONTHIDAU mONTHIDAU = db.MONTHIDAUs.Find(id);
            if (mONTHIDAU == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLOAIMTD = new SelectList(db.LOAIMTDs, "IDLOAIMTD", "TENLOAIMTD", mONTHIDAU.IDMTD);
            return View(mONTHIDAU);
        }

        // POST: Admin/MONTHIDAUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMTD,IDLOAIMTD,TENMTD,MOTA,HINHANH")] MONTHIDAU mONTHIDAU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mONTHIDAU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLOAIMTD = new SelectList(db.LOAIMTDs, "IDLOAIMTD", "TENLOAIMTD", mONTHIDAU.IDMTD);
            return View(mONTHIDAU);
        }

        // GET: Admin/MONTHIDAUs/Delete/5
        public ActionResult Delete(int? id)
        {
            MONTHIDAU mONTHIDAU = db.MONTHIDAUs.Find(id);
            return View(mONTHIDAU);
        }

        // POST: Admin/MONTHIDAUs/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MONTHIDAU mONTHIDAU = db.MONTHIDAUs.Find(id);
            db.MONTHIDAUs.Remove(mONTHIDAU);
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
