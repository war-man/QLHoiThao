using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;

namespace QLHoiThaoProject.Controllers
{
    public class QUYDINHsController : Controller
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: QUYDINHs
        public ActionResult Index()
        {
            var listDSQD = db.QUYDINHs.Include(q => q.MONTHIDAU).ToList().OrderBy(p => p.IDMTD);
            return View(listDSQD);
        }
        
        // GET: QUYDINHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUYDINH qUYDINH = db.QUYDINHs.Find(id);
            if (qUYDINH == null)
            {
                return HttpNotFound();
            }
            return View(qUYDINH);
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
