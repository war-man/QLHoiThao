using System.Linq;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;

namespace QLHoiThaoProject.Controllers
{
    public class MONTHIDAUsController : Controller
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();
        
        // GET: MONTHIDAUs/Details/5
        public ViewResult Details(int IdMTD)
        {
            MONTHIDAU mONTHIDAU = db.MONTHIDAUs.SingleOrDefault(s => s.IDMTD == IdMTD);
            if (mONTHIDAU == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(mONTHIDAU);
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
