using System.Data;
using System.Linq;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
using QLHoiThaoProject.Models;
using QLHoiThaoProject.Common;

namespace QLHoiThaoProject.Controllers
{
    public class THANHVIENsController : Controller
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        
        // GET: THANHVIENs/Create
        public ActionResult Register()
        {
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP");
            return View();
        }

        // POST: THANHVIENs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "IDTV,IDLOP,TENTV,EMAILTV,SDTTV,USERNAME,PASSWORD,TRANGTHAI")] THANHVIEN tHANHVIEN)
        {
            if (ModelState.IsValid)
            {
                if (CheckTrangThai(tHANHVIEN.IDLOP))
                {
                    ModelState.AddModelError("", "Đã tồn tại tài khoản thành viên");
                }                
                //else if (CheckEmail(tHANHVIEN.EMAILTV))
                //{
                //    ModelState.AddModelError("", "Email đã tồn tại");
                //}
                //else if (CheckUserName(tHANHVIEN.USERNAME))
                //{
                //    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                //}
                else
                {
                    var encryptedMd5Pass = Encryptor.MD5Hash(tHANHVIEN.PASSWORD);
                    tHANHVIEN.PASSWORD = encryptedMd5Pass;
                    tHANHVIEN.TRANGTHAI = true;
                    db.THANHVIENs.Add(tHANHVIEN);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
            }

            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", tHANHVIEN.IDLOP);
            return View(tHANHVIEN);
        }

        public bool CheckUserName(string userName)
        {
            return db.THANHVIENs.Count(x => x.USERNAME == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.THANHVIENs.Count(x => x.EMAILTV == email) > 0;
        }
        public bool CheckTrangThai(int idLop)
        {
            var tt = from s in db.THANHVIENs
                     where s.TRANGTHAI == true && s.IDLOP==idLop
                     select s;
            //ViewBag.countTT = tt.ToList().Count();
            //var countTT = L2EQuery.FirstOrDefault<Sinhvien>();
            return tt.ToList().Count() > 0;
        }

        public ActionResult Login()
        {
            return View();
        }

        public int TVLogin(string username, string password)
        {
            var result = db.THANHVIENs.SingleOrDefault(x => x.USERNAME == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.PASSWORD == password)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public THANHVIEN GetById(string userName)
        {
            return db.THANHVIENs.SingleOrDefault(x => x.USERNAME == userName);
        }

        [HttpPost]
        public ActionResult Login(TVLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = TVLogin(model.userName, Encryptor.MD5Hash(model.passWord));
                if (result == 1)
                {
                    var user = GetById(model.userName);
                    var userSession = new TVLogin();
                    userSession.USERNAME = user.USERNAME;
                    userSession.IDTV = user.IDTV;
                    userSession.IDLOP = user.IDLOP;
                    Session.Add(TVCommonConstants.TVUSER_SESSION, userSession);
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
            return View(model);
        }

        public ActionResult Logout()
        {
            Session[TVCommonConstants.TVUSER_SESSION] = null;
            Session[TVCommonConstants.TVLopID_SESSION] = null;
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
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
