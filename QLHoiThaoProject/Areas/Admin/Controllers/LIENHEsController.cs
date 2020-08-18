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
    public class LIENHEsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/LIENHEs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.LIENHEs.ToList().Count;
            return View(db.LIENHEs.ToList().OrderBy(p => p.IDLIENHE).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/LIENHEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIENHE lIENHE = db.LIENHEs.Find(id);
            if (lIENHE == null)
            {
                return HttpNotFound();
            }
            return View(lIENHE);
        }

        
        // GET: Admin/LIENHEs/Delete/5
        public ActionResult Delete(int? id)
        {
            LIENHE lIENHE = db.LIENHEs.Find(id);
            return View(lIENHE);
        }

        // POST: Admin/LIENHEs/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            LIENHE lIENHE = db.LIENHEs.Find(id);
            db.LIENHEs.Remove(lIENHE);
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
