using PagedList;
using QLHoiThaoProject.Models.EFModel;
using System.Linq;
using System.Web.Mvc;

namespace QLHoiThaoProject.Controllers
{
    public class HomeController : Controller
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();
        public ActionResult Index(int? page)
        {
            ViewBag.MTDList = db.COMTDs.ToList().OrderByDescending(m => m.IDHT);
            ViewBag.TBList = db.QUYDINHs.ToList();

            int pageNumber = (page ?? 1);
            int pageSize = 6;
            ViewBag.Count = db.COMTDs.ToList().Count;
            var listDSMTD = db.COMTDs.ToList().OrderByDescending(p => p.IDHT).ToPagedList(pageNumber, pageSize);
            return View(listDSMTD);
        }
    }
}