using QLHoiThaoProject.Areas.Admin.Models;
using System.Linq;
using System.Web.Mvc;
using QLHoiThaoProject.Common;
using QLHoiThaoProject.Models.EFModel;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        QLHoiThaoDbContext db = null;

        public LoginController()
        {
            db = new QLHoiThaoDbContext();
        }
        // GET: Admin/login
        public ActionResult Index()
        {
            return View();
        }
        public int Login(string username, string password)
        {
            var result = db.CBQUANLies.SingleOrDefault(x => x.USERNAME == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.PASWORD == password)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
        
        public CBQUANLY GetById(string userName)
        {
            return db.CBQUANLies.SingleOrDefault(x => x.USERNAME == userName);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Login(model.userName, Encryptor.MD5Hash(model.passWord));
                if (result == 1)
                {
                    var user = GetById(model.userName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.USERNAME;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;//or Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        

    }
}