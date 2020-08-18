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
    public class VONGDAUsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();
                
        // GET: Admin/VONGDAUs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.VONGDAUs.Include(v => v.HOITHAO).Include(v => v.MONTHIDAU).ToList().Count;
            return View(db.VONGDAUs.Include(v => v.HOITHAO).Include(v => v.MONTHIDAU).ToList().OrderBy(p => p.IDVONGDAU).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.VONGDAUs.Include(v => v.HOITHAO).Include(v => v.MONTHIDAU).Where(p => p.TENVONGDAU.Contains(Name) || p.HOITHAO.TENHT.Contains(Name) || p.MONTHIDAU.TENMTD.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDVONGDAU).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/VONGDAUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VONGDAU vONGDAU = db.VONGDAUs.Find(id);
            if (vONGDAU == null)
            {
                return HttpNotFound();
            }
            return View(vONGDAU);
        }

        // GET: Admin/VONGDAUs/Create
        public ActionResult Create()
        {
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT");
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD");
            return View();
        }

        // POST: Admin/VONGDAUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVONGDAU,IDMTD,IDHT,TENVONGDAU")] VONGDAU vONGDAU)
        {
            if (ModelState.IsValid)
            {
                db.VONGDAUs.Add(vONGDAU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", vONGDAU.IDHT);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", vONGDAU.IDMTD);
            return View(vONGDAU);
        }

        // GET: Admin/VONGDAUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VONGDAU vONGDAU = db.VONGDAUs.Find(id);
            if (vONGDAU == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", vONGDAU.IDHT);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", vONGDAU.IDMTD);
            return View(vONGDAU);
        }

        // POST: Admin/VONGDAUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDVONGDAU,IDMTD,IDHT,TENVONGDAU")] VONGDAU vONGDAU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vONGDAU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", vONGDAU.IDHT);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", vONGDAU.IDMTD);
            return View(vONGDAU);
        }

        // GET: Admin/VONGDAUs/Delete/5
        public ActionResult Delete(int? id)
        {
            VONGDAU vONGDAU = db.VONGDAUs.Find(id);
            return View(vONGDAU);
        }

        // POST: Admin/VONGDAUs/Delete/5
        [HttpPost, ActionName("Delete")]        
        public ActionResult DeleteConfirmed(int id)
        {
            VONGDAU vONGDAU = db.VONGDAUs.Find(id);
            db.VONGDAUs.Remove(vONGDAU);
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
