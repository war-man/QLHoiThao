using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using PagedList;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class TRONGTAIsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/TRONGTAIs
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = db.TRONGTAIs.ToList().Count;
            return View(db.TRONGTAIs.ToList().OrderBy(p => p.IDTRONGTAI).ToPagedList(pageNumber, pageSize));
        }
        //Tim kiem thanh vien theo ten
        public ActionResult Search(int? page, string Name = "")
        {
            var model = db.TRONGTAIs.Where(p => p.TENTRONGTAI.Contains(Name) || p.EMAIL.Contains(Name)||p.SDT.Contains(Name));
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            ViewBag.Count = model.ToList().Count;
            return View(model.OrderBy(p => p.IDTRONGTAI).ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/TRONGTAIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRONGTAI tRONGTAI = db.TRONGTAIs.Find(id);
            if (tRONGTAI == null)
            {
                return HttpNotFound();
            }
            return View(tRONGTAI);
        }

        // GET: Admin/TRONGTAIs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TRONGTAIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTRONGTAI,TENTRONGTAI,EMAIL,SDT")] TRONGTAI tRONGTAI)
        {
            if (ModelState.IsValid)
            {
                db.TRONGTAIs.Add(tRONGTAI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tRONGTAI);
        }

        // GET: Admin/TRONGTAIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRONGTAI tRONGTAI = db.TRONGTAIs.Find(id);
            if (tRONGTAI == null)
            {
                return HttpNotFound();
            }
            return View(tRONGTAI);
        }

        // POST: Admin/TRONGTAIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTRONGTAI,TENTRONGTAI,EMAIL,SDT")] TRONGTAI tRONGTAI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRONGTAI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tRONGTAI);
        }

        // GET: Admin/TRONGTAIs/Delete/5
        public ActionResult Delete(int? id)
        {
            TRONGTAI tRONGTAI = db.TRONGTAIs.Find(id);
            return View(tRONGTAI);
        }

        // POST: Admin/TRONGTAIs/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            TRONGTAI tRONGTAI = db.TRONGTAIs.Find(id);
            db.TRONGTAIs.Remove(tRONGTAI);
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
