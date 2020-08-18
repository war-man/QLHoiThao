using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLHoiThaoProject.Models;
using QLHoiThaoProject.Models.EFModel;
using PagedList;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class SINHVIENsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/SINHVIENs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.SINHVIENs.ToList().Count;
            return View(db.SINHVIENs.ToList().OrderBy(p => p.IDLOP).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.SINHVIENs.Where(p => p.HOTEN.Contains(Name) || p.EMAIL.Contains(Name) || p.LOP.TENLOP.Contains(Name) || p.SDT.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDLOP).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SINHVIENs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Create
        public ActionResult Create()
        {
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP");
            return View();
        }

        // POST: Admin/SINHVIENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSV,IDLOP,HOTEN,EMAIL,SDT")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.SINHVIENs.Add(sINHVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", sINHVIEN.IDLOP);
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", sINHVIEN.IDLOP);
            return View(sINHVIEN);
        }

        // POST: Admin/SINHVIENs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSV,IDLOP,HOTEN,EMAIL,SDT")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", sINHVIEN.IDLOP);
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Delete/5
        public ActionResult Delete(int? id)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            return View(sINHVIEN);
        }

        // POST: Admin/SINHVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            db.SINHVIENs.Remove(sINHVIEN);
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
