using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using PagedList;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class GIAITHUONGsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/GIAITHUONGs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.GIAITHUONGs.ToList().Count;
            return View(db.GIAITHUONGs.ToList().OrderBy(p => p.IDGT).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.GIAITHUONGs.Where(p => p.TENGT.Contains(Name) || p.MONTHIDAU.TENMTD.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDGT).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/GIAITHUONGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAITHUONG gIAITHUONG = db.GIAITHUONGs.Find(id);
            if (gIAITHUONG == null)
            {
                return HttpNotFound();
            }
            return View(gIAITHUONG);
        }

        // GET: Admin/GIAITHUONGs/Create
        public ActionResult Create()
        {
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD");
            return View();
        }

        // POST: Admin/GIAITHUONGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGT,IDMTD,TENGT,GIATRIGT")] GIAITHUONG gIAITHUONG)
        {
            if (ModelState.IsValid)
            {
                db.GIAITHUONGs.Add(gIAITHUONG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", gIAITHUONG.IDMTD);
            return View(gIAITHUONG);
        }

        // GET: Admin/GIAITHUONGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAITHUONG gIAITHUONG = db.GIAITHUONGs.Find(id);
            if (gIAITHUONG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", gIAITHUONG.IDMTD);
            return View(gIAITHUONG);
        }

        // POST: Admin/GIAITHUONGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDGT,IDMTD,TENGT,GIATRIGT")] GIAITHUONG gIAITHUONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIAITHUONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", gIAITHUONG.IDMTD);
            return View(gIAITHUONG);
        }

        // GET: Admin/GIAITHUONGs/Delete/5
        public ActionResult Delete(int? id)
        {
            GIAITHUONG gIAITHUONG = db.GIAITHUONGs.Find(id);
            return View(gIAITHUONG);
        }

        // POST: Admin/GIAITHUONGs/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            GIAITHUONG gIAITHUONG = db.GIAITHUONGs.Find(id);
            db.GIAITHUONGs.Remove(gIAITHUONG);
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
