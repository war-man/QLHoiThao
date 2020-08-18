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
    public class QUYDINHsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/QUYDINHs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.QUYDINHs.ToList().Count;
            return View(db.QUYDINHs.ToList().OrderBy(p => p.IDQUYDINH).ToPagedList(pageNumber, pageSize));
        }
        

        // GET: Admin/QUYDINHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUYDINH qUYDINH = db.QUYDINHs.Find(id);
            if (qUYDINH == null)
            {
                return HttpNotFound();
            }
            return View(qUYDINH);
        }

        // GET: Admin/QUYDINHs/Create
        public ActionResult Create()
        {
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD");
            return View();
        }

        // POST: Admin/QUYDINHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDQUYDINH,IDMTD,MOTA")] QUYDINH qUYDINH)
        {
            if (ModelState.IsValid)
            {
                db.QUYDINHs.Add(qUYDINH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", qUYDINH.IDMTD);
            return View(qUYDINH);
        }

        // GET: Admin/QUYDINHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUYDINH qUYDINH = db.QUYDINHs.Find(id);
            if (qUYDINH == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", qUYDINH.IDMTD);
            return View(qUYDINH);
        }

        // POST: Admin/QUYDINHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDQUYDINH,IDMTD,MOTA")] QUYDINH qUYDINH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qUYDINH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", qUYDINH.IDMTD);
            return View(qUYDINH);
        }

        // GET: Admin/QUYDINHs/Delete/5
        public ActionResult Delete(int? id)
        {
            QUYDINH qUYDINH = db.QUYDINHs.Find(id);
            return View(qUYDINH);
        }

        // POST: Admin/QUYDINHs/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            QUYDINH qUYDINH = db.QUYDINHs.Find(id);
            db.QUYDINHs.Remove(qUYDINH);
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
