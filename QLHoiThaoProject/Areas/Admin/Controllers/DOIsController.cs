using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class DOIsController : BaseController
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/DOIs
        //public ActionResult Index()
        //{
        //    var dOIs = db.DOIs.Include(d => d.BANGDAU).Include(d => d.HOITHAO).Include(d => d.LOP).Include(d => d.MONTHIDAU);
        //    return View(dOIs.ToList());
        //}

        // GET: Admin/DOIs
        public ActionResult Index(int? id, int? idMTD)
        {
            var dOIs = db.DOIs.Include(d => d.BANGDAU).Include(d => d.HOITHAO).Include(d => d.LOP).Include(d => d.MONTHIDAU);
            if (id != null && idMTD != null)
            {
                dOIs = db.DOIs.Include(d => d.BANGDAU).Include(d => d.HOITHAO).Include(d => d.LOP).Include(d => d.MONTHIDAU).Where(d => d.IDHT == id && d.IDMTD == idMTD).OrderBy(d => d.IDMTD);

            }
            if (id != null)
            {
                dOIs = db.DOIs.Include(d => d.BANGDAU).Include(d => d.HOITHAO).Include(d => d.LOP).Include(d => d.MONTHIDAU).Where(d => d.IDHT == id).OrderBy(d => d.IDBANG);
            }
            ViewBag.IDMTD = new SelectList(db.COMTDs.Include(m => m.MONTHIDAU).Include(m => m.HOITHAO).Where(m => m.IDHT == id).Select(m => new { IDMTD = m.MONTHIDAU.IDMTD, TENMTD = m.MONTHIDAU.TENMTD }).Distinct(), "IDMTD", "TENMTD");
            return View(dOIs);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(int? id, int text1)
        {
            var dOIs = db.DOIs.Include(d => d.BANGDAU).Include(d => d.HOITHAO).Include(d => d.LOP).Include(d => d.MONTHIDAU).Where(d => d.IDHT == id && d.IDMTD == text1).OrderBy(d => d.IDBANG);
            ViewBag.IDMTD = new SelectList(db.COMTDs.Include(m => m.MONTHIDAU).Include(m => m.HOITHAO).Where(m => m.IDHT == id).Select(m => new { IDMTD = m.MONTHIDAU.IDMTD, TENMTD = m.MONTHIDAU.TENMTD }).Distinct(), "IDMTD", "TENMTD");
            return View(dOIs);
        }

        // GET: Admin/DOIs/TimHT/
        public ActionResult TimHT()
        {
            ViewBag.IDHT = db.HOITHAOs.ToList();
            return View();
        }

        //tìm các đội thi đấu theo môn nào trong kì hội thao nào
        public ActionResult TimMTD(int? id)
        {
            ViewBag.IDMTD = db.DOIs.Where(d => d.IDHT == id).ToList();//MONTHI_HT.ToList();
            return View();
        }

        public ActionResult ThaoTacTV(int? id, int? idlop, int? idsv, int? idtt)
        {
            if (idsv == null)
            {
                ViewBag.DANHSACHTV = db.THUOCDOIs.Where(t => t.IDDOI == id);
                ViewBag.DANHSACHSV = db.SINHVIENs.Where(s => s.IDLOP == idlop);
                //ViewData.Add("MaDoi", id);
            }
            else
            {
                if (idtt == null)
                {
                    ThemTVDoi(id, idlop, idsv);
                    ViewBag.DANHSACHTV = db.THUOCDOIs.Where(t => t.IDDOI == id);
                    ViewBag.DANHSACHSV = db.SINHVIENs.Where(s => s.IDLOP == idlop);
                }
            }

            if (idtt != null)
            {
                XoaTVDoi(id, idsv, idtt);
                ViewBag.DANHSACHTV = db.THUOCDOIs.Where(t => t.DOI.IDDOI == id);
                ViewBag.DANHSACHSV = db.SINHVIENs.Where(s => s.IDLOP == idlop);
                //ViewData.Add("MaDoi", id);
            }

            var TenDoi = db.DOIs.Where(d => d.IDDOI == id).Select(d => new { TENDOI = d.TENDOI }).SingleOrDefault();
            var IDHoiThao = db.DOIs.Where(d => d.IDDOI == id).Select(d => new { d.IDHT }).SingleOrDefault();
            ViewData.Add("IDHoiThao", IDHoiThao.IDHT);
            ViewData.Add("TenDoi", TenDoi.TENDOI);
            ViewData.Add("MaLop", idlop);
            ViewData.Add("MaDoi", id);
            return View();
        }

        public JsonResult XoaTVDoi(int? id, int? idsv, int? idtt)
        {
            var IdThuocDoi = db.THUOCDOIs.Where(t => t.IDDOI == id && t.THANHVIENDOI.IDSV == idsv).Select(t => new { t.IDDOI, t.IDVDVDOI }).SingleOrDefault();
            THUOCDOI thuocdoi = db.THUOCDOIs.Find(IdThuocDoi.IDDOI, IdThuocDoi.IDVDVDOI);
            if (idtt != null)
            {
                db.THUOCDOIs.Remove(thuocdoi);//xóa khỏi bảng thuộc đội thành viên có idsv
                db.SaveChanges();

                //nếu như trong bảng thuocdoi  không tìm thấy thành viên có idsv nữa thì xóa thành viên này trong bảng
                //thành viên đội
                var KT_MonKhac = db.THUOCDOIs.Where(t => t.IDVDVDOI == IdThuocDoi.IDVDVDOI);
                if (KT_MonKhac.Count() == 0)
                {
                    THANHVIENDOI thanhviendoi = db.THANHVIENDOIs.Find(IdThuocDoi.IDVDVDOI);
                    db.THANHVIENDOIs.Remove(thanhviendoi);
                    db.SaveChanges();
                }
            }
            return Json(new { status = "xóa thành viên thành công" });
        }

        public JsonResult ThemTVDoi(int? id, int? idlop, int? idsv)
        {
            try
            {
                if (idsv != null)
                {
                    //lấy ra thông tin sinh viên theo mã sinh viên truyền vào
                    var TTSinhVien = db.SINHVIENs.Where(s => s.IDSV == idsv).Select(s => new { s.HOTEN, s.SDT, s.EMAIL }).SingleOrDefault();
                    var Hoten = TTSinhVien.HOTEN;
                    var sdt = TTSinhVien.SDT;
                    var email = TTSinhVien.EMAIL;

                    //lấy ra mã thành viên đội<> nếu đã có trong bảng thành viên thì không add vào bảng thành viên mà kiểm tra dk
                    //để add vào bảng THUOCDOI
                    var Test_PR_ThanhVienDoi = db.THANHVIENDOIs.Where(t => t.IDSV == idsv).SingleOrDefault();


                    //khi kiểm tra mà sinh viên đó chưa có trong bảng THANHVIENDOI
                    if (Test_PR_ThanhVienDoi == null)
                    {
                        //lấy thông tin và lưu sinh viên vào bảng THANHVIENDOI
                        THANHVIENDOI tvd = new THANHVIENDOI();
                        tvd.IDSV = (int)idsv;
                        tvd.TENVDV = Hoten;
                        tvd.SDTVDV = sdt;
                        tvd.EMAILVDV = email;
                        db.THANHVIENDOIs.Add(tvd); db.SaveChanges();

                        //lấy ra IDVDVDOI của sinh viên vừa truyền vào bảng đội.
                        var ID_ThanhVienDoi = db.THANHVIENDOIs.Where(t => t.IDSV == idsv).Select(t => new { t.IDVDVDOI }).SingleOrDefault();
                        var idvdv = ID_ThanhVienDoi.IDVDVDOI;
                        THUOCDOI td = new THUOCDOI();
                        td.IDVDVDOI = idvdv;
                        td.IDDOI = (int)id;
                        td.GHICHU = "";
                        db.THUOCDOIs.Add(td); db.SaveChanges();
                    }
                    else
                    {//khi kiểm tra và phát hiện sinh viên này đã có trong bảng THANHVIENDOI
                        var Test_PR_Doi = db.THUOCDOIs.Where(t => t.IDDOI == id && t.THANHVIENDOI.IDSV == idsv).SingleOrDefault();
                        //kiểm tra xem có đội mà sinh viên đăng kí đã có chưa, nếu chưa có thì thêm vào bảng THUOCDOI.
                        if (Test_PR_Doi == null)
                        {
                            var ID_ThanhVienDoi = db.THANHVIENDOIs.Where(t => t.IDSV == idsv).Select(t => new { t.IDVDVDOI }).SingleOrDefault();
                            var idvdv = ID_ThanhVienDoi.IDVDVDOI;
                            THUOCDOI td = new THUOCDOI();
                            td.IDVDVDOI = idvdv;
                            td.IDDOI = (int)id;
                            td.GHICHU = "";
                            db.THUOCDOIs.Add(td); db.SaveChanges();
                        }
                    }

                }
                return Json(new { status = "Thêm thành viên thành công" });
            }
            catch (Exception)
            {
                return Json(new { status = "Có lỗi xảy ra." });
            }
        }


        // GET: Admin/DOIs/Details/5
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

        // GET: Admin/DOIs/Create
        public ActionResult Create()
        {
            ViewBag.IDBANG = new SelectList(db.BANGDAUs, "IDBANG", "TENBANG");
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT");
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP");
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD");
            return View();
        }

        // POST: Admin/DOIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDOI,IDHT,IDMTD,IDBANG,IDLOP,BILOAI,TENDOI")] DOI dOI)
        {
            if (ModelState.IsValid)
            {
                db.DOIs.Add(dOI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBANG = new SelectList(db.BANGDAUs, "IDBANG", "TENBANG", dOI.IDBANG);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", dOI.IDHT);
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", dOI.IDLOP);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", dOI.IDMTD);
            return View(dOI);
        }

        // GET: Admin/DOIs/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IDBANG = new SelectList(db.BANGDAUs, "IDBANG", "TENBANG", dOI.IDBANG);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", dOI.IDHT);
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", dOI.IDLOP);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", dOI.IDMTD);
            return View(dOI);
        }

        // POST: Admin/DOIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDOI,IDHT,IDMTD,IDBANG,IDLOP,BILOAI,TENDOI")] DOI dOI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBANG = new SelectList(db.BANGDAUs, "IDBANG", "TENBANG", dOI.IDBANG);
            ViewBag.IDHT = new SelectList(db.HOITHAOs, "IDHT", "TENHT", dOI.IDHT);
            ViewBag.IDLOP = new SelectList(db.LOPs, "IDLOP", "TENLOP", dOI.IDLOP);
            ViewBag.IDMTD = new SelectList(db.MONTHIDAUs, "IDMTD", "TENMTD", dOI.IDMTD);
            return View(dOI);
        }

        // GET: Admin/DOIs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/DOIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DOI dOI = db.DOIs.Find(id);
            db.DOIs.Remove(dOI);
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
