using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using PagedList;
using QLHoiThaoProject.Common;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class CBQUANLiesController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/CBQUANLies
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.CBQUANLies.ToList().Count;
            return View(db.CBQUANLies.ToList().OrderBy(p => p.ID).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.CBQUANLies.Where(p => p.TENCB.Contains(Name) || p.SDTCB.Contains(Name) || p.EMAILCB.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.ID).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/CBQUANLies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CBQUANLY cBQUANLY = db.CBQUANLies.Find(id);
            if (cBQUANLY == null)
            {
                return HttpNotFound();
            }
            return View(cBQUANLY);
        }

        // GET: Admin/CBQUANLies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CBQUANLies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TENCB,EMAILCB,SDTCB,USERNAME,PASWORD")] CBQUANLY cBQUANLY)
        {
            if (ModelState.IsValid)
            {
                var encryptedMd5Pass = Encryptor.MD5Hash(cBQUANLY.PASWORD);
                cBQUANLY.PASWORD = encryptedMd5Pass;
                db.CBQUANLies.Add(cBQUANLY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cBQUANLY);
        }

        // GET: Admin/CBQUANLies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CBQUANLY cBQUANLY = db.CBQUANLies.Find(id);
            if (cBQUANLY == null)
            {
                return HttpNotFound();
            }
            return View(cBQUANLY);
        }

        // POST: Admin/CBQUANLies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TENCB,EMAILCB,SDTCB,USERNAME,PASWORD")] CBQUANLY cBQUANLY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cBQUANLY).State = EntityState.Modified;
                var encryptedMd5Pass = Encryptor.MD5Hash(cBQUANLY.PASWORD);
                cBQUANLY.PASWORD = encryptedMd5Pass;
                db.SaveChanges();
                return RedirectToAction("Index");                
            }
            return View(cBQUANLY);
        }

        // GET: Admin/CBQUANLies/Delete/5
        public ActionResult Delete(int? id)
        {
            CBQUANLY cBQUANLY = db.CBQUANLies.Find(id);
            return View(cBQUANLY);
        }

        // POST: Admin/CBQUANLies/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CBQUANLY cBQUANLY = db.CBQUANLies.Find(id);
            db.CBQUANLies.Remove(cBQUANLY);
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
