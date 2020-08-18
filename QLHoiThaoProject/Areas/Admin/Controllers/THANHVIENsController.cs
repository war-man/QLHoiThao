using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using PagedList;
using QLHoiThaoProject.Common;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class THANHVIENsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/THANHVIENs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.THANHVIENs.Include(t => t.LOP).ToList().Count;
            return View(db.THANHVIENs.Include(t => t.LOP).ToList().OrderBy(p => p.IDTV).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.THANHVIENs.Include(t => t.LOP).Where(p => p.TENTV.Contains(Name) || p.EMAILTV.Contains(Name) || p.SDTTV.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDTV).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/THANHVIENs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANHVIEN tHANHVIEN = db.THANHVIENs.Find(id);
            if (tHANHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(tHANHVIEN);
        }

        // GET: Admin/THANHVIENs/Create
        public ActionResult Create()
        {
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP");
            return View();
        }

        // POST: Admin/THANHVIENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTV,IDLOP,TENTV,EMAILTV,SDTTV,USERNAME,PASSWORD,TRANGTHAI")] THANHVIEN tHANHVIEN)
        {
            if (ModelState.IsValid)
            {
                var encryptedMd5Pass = Encryptor.MD5Hash(tHANHVIEN.PASSWORD);
                tHANHVIEN.PASSWORD = encryptedMd5Pass;
                db.THANHVIENs.Add(tHANHVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", tHANHVIEN.IDLOP);
            return View(tHANHVIEN);
        }

        // GET: Admin/THANHVIENs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANHVIEN tHANHVIEN = db.THANHVIENs.Find(id);
            if (tHANHVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", tHANHVIEN.IDLOP);
            return View(tHANHVIEN);
        }

        // POST: Admin/THANHVIENs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTV,IDLOP,TENTV,EMAILTV,SDTTV,USERNAME,PASSWORD,TRANGTHAI")] THANHVIEN tHANHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHANHVIEN).State = EntityState.Modified;
                var encryptedMd5Pass = Encryptor.MD5Hash(tHANHVIEN.PASSWORD);
                tHANHVIEN.PASSWORD = encryptedMd5Pass;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", tHANHVIEN.IDLOP);
            return View(tHANHVIEN);
        }

        // GET: Admin/THANHVIENs/Delete/5
        public ActionResult Delete(int id)
        {
            THANHVIEN tHANHVIEN = db.THANHVIENs.Find(id);
            return View(tHANHVIEN);
        }

        // POST: Admin/THANHVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THANHVIEN tHANHVIEN = db.THANHVIENs.Find(id);
            db.THANHVIENs.Remove(tHANHVIEN);
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
