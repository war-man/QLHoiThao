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
    public class LOAIMTDsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/LOAIMTDs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.LOAIMTDs.ToList().Count;
            return View(db.LOAIMTDs.ToList().OrderBy(p => p.IDLOAIMTD).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.LOAIMTDs.Where(p => p.TENLOAIMTD.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDLOAIMTD).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/LOAIMTDs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMTD lOAIMTD = db.LOAIMTDs.Find(id);
            if (lOAIMTD == null)
            {
                return HttpNotFound();
            }
            return View(lOAIMTD);
        }

        // GET: Admin/LOAIMTDs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LOAIMTDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLOAIMTD,TENLOAIMTD,MOTA")] LOAIMTD lOAIMTD)
        {
            if (ModelState.IsValid)
            {
                db.LOAIMTDs.Add(lOAIMTD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAIMTD);
        }

        // GET: Admin/LOAIMTDs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMTD lOAIMTD = db.LOAIMTDs.Find(id);
            if (lOAIMTD == null)
            {
                return HttpNotFound();
            }
            return View(lOAIMTD);
        }

        // POST: Admin/LOAIMTDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLOAIMTD,TENLOAIMTD,MOTA")] LOAIMTD lOAIMTD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAIMTD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAIMTD);
        }

        // GET: Admin/LOAIMTDs/Delete/5
        public ActionResult Delete(int? id)
        {
            LOAIMTD lOAIMTD = db.LOAIMTDs.Find(id);
            return View(lOAIMTD);
        }

        // POST: Admin/LOAIMTDs/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            LOAIMTD lOAIMTD = db.LOAIMTDs.Find(id);
            db.LOAIMTDs.Remove(lOAIMTD);
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
