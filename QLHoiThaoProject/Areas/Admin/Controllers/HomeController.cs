using QLHoiThaoProject.Common;
using System.Web.Mvc;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/hOME
        public ActionResult Index()
        {
            if (Session[CommonConstants.USER_SESSION] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

       
    }
}