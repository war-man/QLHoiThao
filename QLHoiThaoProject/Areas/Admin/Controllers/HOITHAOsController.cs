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
    public class HOITHAOsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/HOITHAOs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.HOITHAOs.ToList().Count;
            return View(db.HOITHAOs.ToList().OrderBy(p => p.IDHT).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.HOITHAOs.Where(p => p.TENHT.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDHT).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/HOITHAOs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOITHAO hOITHAO = db.HOITHAOs.Find(id);
            if (hOITHAO == null)
            {
                return HttpNotFound();
            }
            return View(hOITHAO);
        }

        // GET: Admin/HOITHAOs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HOITHAOs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHT,TENHT,TUNGAY,DENNGAY,NGAYBDDK,NGAYKTDK")] HOITHAO hOITHAO)
        {
            if (ModelState.IsValid)
            {
                db.HOITHAOs.Add(hOITHAO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hOITHAO);
        }

        // GET: Admin/HOITHAOs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOITHAO hOITHAO = db.HOITHAOs.Find(id);
            if (hOITHAO == null)
            {
                return HttpNotFound();
            }
            return View(hOITHAO);
        }

        // POST: Admin/HOITHAOs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHT,TENHT,TUNGAY,DENNGAY,NGAYBDDK,NGAYKTDK")] HOITHAO hOITHAO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOITHAO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hOITHAO);
        }

        // GET: Admin/HOITHAOs/Delete/5
        public ActionResult Delete(int? id)
        {
            HOITHAO hOITHAO = db.HOITHAOs.Find(id);
            return View(hOITHAO);
        }

        // POST: Admin/HOITHAOs/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HOITHAO hOITHAO = db.HOITHAOs.Find(id);
            db.HOITHAOs.Remove(hOITHAO);
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
