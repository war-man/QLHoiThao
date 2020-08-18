using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using PagedList;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class HINHANHsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/HINHANHs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.HINHANHs.Include(h => h.TRANDAU).ToList().Count;
            return View(db.HINHANHs.Include(h => h.TRANDAU).ToList().OrderBy(p => p.IDHA).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/HINHANHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANH hINHANH = db.HINHANHs.Find(id);
            if (hINHANH == null)
            {
                return HttpNotFound();
            }
            return View(hINHANH);
        }

        // GET: Admin/HINHANHs/Create
        public ActionResult Create()
        {
            ViewBag.IDTD = new SelectList(db.TRANDAUs, "IDTD", "IDTD");
            return View();
        }

        // POST: Admin/HINHANHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHA,IDTD,TENHA,LINK")] HINHANH hINHANH)
        {
            if (ModelState.IsValid)
            {
                db.HINHANHs.Add(hINHANH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTD = new SelectList(db.TRANDAUs, "IDTD", "IDTD", hINHANH.IDTD);
            return View(hINHANH);
        }

        // GET: Admin/HINHANHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHANH hINHANH = db.HINHANHs.Find(id);
            if (hINHANH == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTD = new SelectList(db.TRANDAUs, "IDTD", "IDTD", hINHANH.IDTD);
            return View(hINHANH);
        }

        // POST: Admin/HINHANHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHA,IDTD,TENHA,LINK")] HINHANH hINHANH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hINHANH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTD = new SelectList(db.TRANDAUs, "IDTD", "IDTD", hINHANH.IDTD);
            return View(hINHANH);
        }

        // GET: Admin/HINHANHs/Delete/5
        public ActionResult Delete(int? id)
        {
            HINHANH hINHANH = db.HINHANHs.Find(id);
            return View(hINHANH);
        }

        // POST: Admin/HINHANHs/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HINHANH hINHANH = db.HINHANHs.Find(id);
            db.HINHANHs.Remove(hINHANH);
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
