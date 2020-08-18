using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;

namespace QLHoiThaoProject.Controllers
{
    public class LIENHEsController : Controller
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();
        
        // GET: LIENHEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LIENHEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLIENHE,DONVI,TIEUDE,NOIDUNG")] LIENHE lIENHE)
        {
            if (ModelState.IsValid)
            {
                db.LIENHEs.Add(lIENHE);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(lIENHE);
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
