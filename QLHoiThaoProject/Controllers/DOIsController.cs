using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;

namespace QLHoiThaoProject.Controllers
{
    public class DOIsController : Controller
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: DOIs
        public ActionResult Index()
        {
            var dOIs = db.DOIs.Include(d => d.BANGDAU).Include(d => d.HOITHAO).Include(d => d.LOP).Include(d => d.MONTHIDAU);
            return View(dOIs.ToList());
        }

        // GET: DOIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOI dOI = db.DOIs.Find(id);
            if (dOI == null)
            {
                return HttpNotFound();
            }
            return View(dOI);
        }

        // GET: DOIs/Create
        public ActionResult Register()
        {
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT").OrderByDescending(t=>t.Value);
            
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD");
            return View();
        }

        // POST: DOIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "IDDOI,IDHT,IDMTD,IDLOP,TENDOI")] DOI dOI)
        {
            if (ModelState.IsValid)
            {
                var session = (Common.TVLogin)Session[Common.TVCommonConstants.TVUSER_SESSION];
                dOI.IDLOP = session.IDLOP;
                db.DOIs.Add(dOI);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index","Home");
            }

            //ViewBag.IDBANG = new SelectList(db.BANGDAUs, "IDBANG", "TENBANG", dOI.IDBANG);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", dOI.IDHT);
            //ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", dOI.IDLOP);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", dOI.IDMTD);
            return View(dOI);
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
