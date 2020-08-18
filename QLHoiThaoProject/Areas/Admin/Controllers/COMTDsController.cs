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
    public class COMTDsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/COMTDs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.COMTDs.Include(c => c.HOITHAO).Include(c => c.MONTHIDAU).ToList().Count;
            return View(db.COMTDs.Include(c => c.HOITHAO).Include(c => c.MONTHIDAU).ToList().ToPagedList(pageNumber, pageSize));
        }
        // GET: Admin/COMTDs/Details/5
        public ActionResult Details(int? id, int? idht)
        {
            if (id == null || idht==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMTD cOMTD = db.COMTDs.Find(id,idht);
            if (cOMTD == null)
            {
                return HttpNotFound();
            }
            return View(cOMTD);
        }

        // GET: Admin/COMTDs/Create
        public ActionResult Create()
        {
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT");
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD");
            return View();
        }

        // POST: Admin/COMTDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHT,IDMTD,GHICHU")] COMTD cOMTD)
        {
            if (ModelState.IsValid)
            {
                db.COMTDs.Add(cOMTD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", cOMTD.IDHT);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", cOMTD.IDMTD);
            return View(cOMTD);
        }

        // GET: Admin/COMTDs/Edit/5
        public ActionResult Edit(int? id, int? idht)
        {
            if (id == null || idht==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMTD cOMTD = db.COMTDs.Find(id, idht);
            if (cOMTD == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", cOMTD.IDHT);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", cOMTD.IDMTD);
            return View(cOMTD);
        }

        // POST: Admin/COMTDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHT,IDMTD,GHICHU")] COMTD cOMTD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMTD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", cOMTD.IDHT);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", cOMTD.IDMTD);
            return View(cOMTD);
        }

        // GET: Admin/COMTDs/Delete/5
        public ActionResult Delete(int? id, int? idht)
        {
            if (id == null || idht==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMTD cOMTD = db.COMTDs.Find(id,idht);
            if (cOMTD == null)
            {
                return HttpNotFound();
            }
            return View(cOMTD);
        }

        // POST: Admin/COMTDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idht)
        {
            COMTD cOMTD = db.COMTDs.Find(id,idht);
            db.COMTDs.Remove(cOMTD);
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
