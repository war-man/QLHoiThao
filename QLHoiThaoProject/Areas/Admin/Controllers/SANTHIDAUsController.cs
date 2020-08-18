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
    public class SANTHIDAUsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/SANTHIDAUs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.SANTHIDAUs.ToList().Count;
            return View(db.SANTHIDAUs.ToList().OrderBy(p => p.IDSTD).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.SANTHIDAUs.Where(p => p.TENSTD.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDSTD).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SANTHIDAUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANTHIDAU sANTHIDAU = db.SANTHIDAUs.Find(id);
            if (sANTHIDAU == null)
            {
                return HttpNotFound();
            }
            return View(sANTHIDAU);
        }

        // GET: Admin/SANTHIDAUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SANTHIDAUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSTD,TENSTD,MOTA")] SANTHIDAU sANTHIDAU)
        {
            if (ModelState.IsValid)
            {
                db.SANTHIDAUs.Add(sANTHIDAU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sANTHIDAU);
        }

        // GET: Admin/SANTHIDAUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANTHIDAU sANTHIDAU = db.SANTHIDAUs.Find(id);
            if (sANTHIDAU == null)
            {
                return HttpNotFound();
            }
            return View(sANTHIDAU);
        }

        // POST: Admin/SANTHIDAUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSTD,TENSTD,MOTA")] SANTHIDAU sANTHIDAU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANTHIDAU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sANTHIDAU);
        }

        // GET: Admin/SANTHIDAUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANTHIDAU sANTHIDAU = db.SANTHIDAUs.Find(id);
            if (sANTHIDAU == null)
            {
                return HttpNotFound();
            }
            return View(sANTHIDAU);
        }

        // POST: Admin/SANTHIDAUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANTHIDAU sANTHIDAU = db.SANTHIDAUs.Find(id);
            db.SANTHIDAUs.Remove(sANTHIDAU);
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
