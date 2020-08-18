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
    public class KINHPHIHOTROesController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/KINHPHIHOTROes
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.KINHPHIHOTROes.Include(k => k.MONTHIDAU).Include(k=>k.HOITHAO).ToList().Count;
            return View(db.KINHPHIHOTROes.Include(k => k.MONTHIDAU).Include(k => k.HOITHAO).ToList().OrderBy(p => p.IDKINHPHI).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/KINHPHIHOTROes1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KINHPHIHOTRO kINHPHIHOTRO = db.KINHPHIHOTROes.Find(id);
            if (kINHPHIHOTRO == null)
            {
                return HttpNotFound();
            }
            return View(kINHPHIHOTRO);
        }

        // GET: Admin/KINHPHIHOTROes1/Create
        public ActionResult Create()
        {
            ViewBag.TenMTD = db.MONTHIDAUs.ToList();

            ViewBag.IDMTD = new SelectList(db.COMTDs, "IDMTD","IDMTD");
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT");
            return View();
        }

        // POST: Admin/KINHPHIHOTROes1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKINHPHI,IDHT,IDMTD,GIATRI")] KINHPHIHOTRO kINHPHIHOTRO)
        {
            if (ModelState.IsValid)
            {
                db.KINHPHIHOTROes.Add(kINHPHIHOTRO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.IDMTD = new SelectList(db.COMTDs, "IDMTD", "IDMTD",kINHPHIHOTRO.IDMTD);
            //ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", kINHPHIHOTRO.IDMTD);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT");
            return View(kINHPHIHOTRO);
        }

        // GET: Admin/KINHPHIHOTROes1/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.TenMTD = db.MONTHIDAUs.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KINHPHIHOTRO kINHPHIHOTRO = db.KINHPHIHOTROes.Find(id);
            if (kINHPHIHOTRO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDMTD = new SelectList(db.COMTDs, "IDMTD", "IDMTD", kINHPHIHOTRO.IDMTD);
            //ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", kINHPHIHOTRO.IDMTD);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT",kINHPHIHOTRO.IDHT);
            return View(kINHPHIHOTRO);
        }

        // POST: Admin/KINHPHIHOTROes1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKINHPHI,IDHT,IDMTD,GIATRI")] KINHPHIHOTRO kINHPHIHOTRO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kINHPHIHOTRO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDMTD = new SelectList(db.COMTDs, "IDMTD", "IDMTD", kINHPHIHOTRO.IDMTD);
            //ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", kINHPHIHOTRO.IDMTD);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", kINHPHIHOTRO.IDHT);
            return View(kINHPHIHOTRO);
        }

        // GET: Admin/KINHPHIHOTROes1/Delete/5
        public ActionResult Delete(int? id)
        {
            KINHPHIHOTRO kINHPHIHOTRO = db.KINHPHIHOTROes.Find(id);
            return View(kINHPHIHOTRO);
        }

        // POST: Admin/KINHPHIHOTROes1/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            KINHPHIHOTRO kINHPHIHOTRO = db.KINHPHIHOTROes.Find(id);
            db.KINHPHIHOTROes.Remove(kINHPHIHOTRO);
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
