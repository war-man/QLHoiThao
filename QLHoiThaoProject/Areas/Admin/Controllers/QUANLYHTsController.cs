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
    public class QUANLYHTsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/QUANLYHTs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.QUANLYHTs.Include(q => q.CBQUANLY).Include(q => q.HOITHAO).ToList().Count;
            return View(db.QUANLYHTs.Include(q => q.CBQUANLY).Include(q => q.HOITHAO).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/QUANLYHTs/Details/5
        public ActionResult Details(int? id, int? idht)
        {
            if (id == null || idht==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUANLYHT qUANLYHT = db.QUANLYHTs.Find(id,idht);
            if (qUANLYHT == null)
            {
                return HttpNotFound();
            }
            return View(qUANLYHT);
        }

        // GET: Admin/QUANLYHTs/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.CBQUANLies, "ID", "TENCB");
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT");
            return View();
        }

        // POST: Admin/QUANLYHTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDHT,GHICHU")] QUANLYHT qUANLYHT)
        {
            if (ModelState.IsValid)
            {
                db.QUANLYHTs.Add(qUANLYHT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.CBQUANLies, "ID", "TENCB", qUANLYHT.ID);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", qUANLYHT.IDHT);
            return View(qUANLYHT);
        }

        // GET: Admin/QUANLYHTs/Edit/5
        public ActionResult Edit(int? id, int? idht)
        {
            if (id == null || idht==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUANLYHT qUANLYHT = db.QUANLYHTs.Find(id,idht);
            if (qUANLYHT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.CBQUANLies, "ID", "TENCB", qUANLYHT.ID);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", qUANLYHT.IDHT);
            return View(qUANLYHT);
        }

        // POST: Admin/QUANLYHTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDHT,GHICHU")] QUANLYHT qUANLYHT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qUANLYHT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.CBQUANLies, "ID", "TENCB", qUANLYHT.ID);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", qUANLYHT.IDHT);
            return View(qUANLYHT);
        }

        // GET: Admin/QUANLYHTs/Delete/5
        public ActionResult Delete(int? id, int? idht)
        {
            if (id == null||idht==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUANLYHT qUANLYHT = db.QUANLYHTs.Find(id,idht);
            if (qUANLYHT == null)
            {
                return HttpNotFound();
            }
            return View(qUANLYHT);
        }

        // POST: Admin/QUANLYHTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idht)
        {
            QUANLYHT qUANLYHT = db.QUANLYHTs.Find(id,idht);
            db.QUANLYHTs.Remove(qUANLYHT);
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
