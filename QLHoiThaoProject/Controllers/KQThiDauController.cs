using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLHoiThaoProject.Models.EFModel;
namespace QLHoiThaoProject.Controllers
{
    public class KQThiDauController : Controller
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/CTTRANDAUs1
        public ActionResult Index(int? id)//id vòng đấu
        {
            var cTTRANDAUs = db.CTTRANDAUs.Include(c => c.DOI).
                Include(c => c.TRANDAU).Where(c => c.TRANDAU.TRANGTHAI == true).Where(c => c.TRANDAU.IDVONGDAU == id);
            var tenmon = db.VONGDAUs.Where(v => v.IDVONGDAU == id).Select(v => new { TENMTD = v.MONTHIDAU.TENMTD }).SingleOrDefault();
            ViewData["TenMon"] = tenmon.TENMTD;
            return View(cTTRANDAUs.ToList());
        }

        public ActionResult TimHT_TranDau()
        {
            ViewBag.IDHT = db.HOITHAOs.ToList();
            return View();
        }

        public ActionResult TimMTD_TranDau(int? id)
        {
            //ViewBag.IDBANG = db.BANGDAUs.ToList();            
            var TENHT = db.HOITHAOs.Where(h => h.IDHT == id).Select(h => new { TENHT = h.TENHT }).SingleOrDefault();
            ViewData.Add("TenHT", TENHT.TENHT);
            var Mon = db.VONGDAUs.Where(v => v.IDHT == id).Select(v => new { IDMTD = v.MONTHIDAU.IDMTD, TENMTD = v.MONTHIDAU.TENMTD }).Distinct();
            ViewBag.IDMTD = new SelectList(Mon, "IDMTD", "TENMTD");

            var VONGDAU = db.VONGDAUs.Where(v => v.IDHT == id);
            ViewBag.VONGDAU = VONGDAU;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TimMTD_TranDau(int? id, int IDMTD)
        {
            //ViewBag.IDBANG = db.BANGDAUs.ToList();            
            var VONGDAU = db.VONGDAUs.Where(v => v.IDHT == id && v.IDMTD == IDMTD);
            ViewBag.VONGDAU = VONGDAU;

            var Mon = db.VONGDAUs.Where(v => v.IDHT == id).Select(v => new { IDMTD = v.MONTHIDAU.IDMTD, TENMTD = v.MONTHIDAU.TENMTD }).Distinct();
            ViewBag.IDMTD = new SelectList(Mon, "IDMTD", "TENMTD");
            return View();
        }

        public ActionResult LichTranDau(int? id)//id vong dau
        {
            //lấy hết các mẩu tin trong bảng cttrandau
            ViewBag.LICHDAU =
                db.CTTRANDAUs.Where(ct => ct.TRANDAU.VONGDAU.IDVONGDAU == id).OrderBy(ct => ct.TRANDAU.NGAYTHIDAU).ToList();

            //lấy hết các mẩu tin trong trận đấu
            var TranDau = db.TRANDAUs.Where(ct => ct.VONGDAU.IDVONGDAU == id && ct.TRANGTHAI == true).OrderBy(ct => ct.NGAYTHIDAU).ToList();

            List<string> listtrongtai = new List<string>();

            ViewBag.TRONGTAI = listtrongtai;
            //tổng số mẩu tin trong bảng trận đấu
            ViewBag.Total = TranDau.Count;

            ViewBag.TRANDAU = TranDau;

            return View();
        }

        public ActionResult Edit(int? id)
        {//id ma tran dau
            var trandau = db.TRANDAUs.Where(ct => ct.IDTD == id).OrderBy(ct => ct.NGAYTHIDAU).ToList().SingleOrDefault();
            ViewBag.TRAN = trandau;
            var cttran = db.CTTRANDAUs.Where(ct => ct.IDTD == id).ToList();
            ViewBag.CTTRAN = cttran;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int txtTiSo1, int txtTiSo2, int txtSoDiem1, int txtSoDiem2)
        {
            //CTTRANDAU ChiTiet = new CTTRANDAU();            
            var cttran = db.CTTRANDAUs.Where(ct => ct.IDTD == id).ToList();
            int a = 0; int b = 0;
            for (int i = 0; i < 1; i++)
            {
                a = cttran[i].IDDOI;//id doi 1
                b = cttran[i + 1].IDDOI;// id doi 2
            }
            CTTRANDAU ctt = db.CTTRANDAUs.Single(c => c.IDTD == id && c.IDDOI == a);
            ctt.TISO = txtTiSo1.ToString(); ctt.DIEM = txtSoDiem1;

            CTTRANDAU ctt1 = db.CTTRANDAUs.Single(c => c.IDTD == id && c.IDDOI == b);
            ctt1.TISO = txtTiSo2.ToString(); ctt1.DIEM = txtSoDiem2;

            db.SaveChanges();

            ViewBag.CTTRAN = db.CTTRANDAUs.Where(ct => ct.IDTD == id).ToList();
            ViewBag.DOITHANG = new SelectList(db.CTTRANDAUs.Where(c => c.IDTD == id).Select(c => new { IDDOI = c.IDDOI, TENDOI = c.DOI.TENDOI }), "IDDOI", "TENDOI");
            return View();
        }

        // GET: Admin/CTTRANDAUs1/Details/5
        public ActionResult Details(int? id, int? idtd)
        {
            if (id == null || idtd == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTTRANDAU cTTRANDAU = db.CTTRANDAUs.Find(id, idtd);
            if (cTTRANDAU == null)
            {
                return HttpNotFound();
            }
            return View(cTTRANDAU);
        }

        // GET: Admin/CTTRANDAUs1/Create
        public ActionResult Create()
        {
            ViewBag.IDDOI = new SelectList(db.DOIs, "IDDOI", "TENDOI");
            ViewBag.IDTD = new SelectList(db.TRANDAUs, "IDTD", "THOIGIANBD");
            return View();
        }

        // POST: Admin/CTTRANDAUs1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDOI,IDTD,KETQUA,TISO,DIEM")] CTTRANDAU cTTRANDAU)
        {
            if (ModelState.IsValid)
            {
                db.CTTRANDAUs.Add(cTTRANDAU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDOI = new SelectList(db.DOIs, "IDDOI", "TENDOI", cTTRANDAU.IDDOI);
            ViewBag.IDTD = new SelectList(db.TRANDAUs, "IDTD", "THOIGIANBD", cTTRANDAU.IDTD);
            return View(cTTRANDAU);
        }

        // GET: Admin/CTTRANDAUs1/Edit/5
        //public ActionResult Edit(int? id, int? idtd)
        //{
        //    if (id == null || idtd == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CTTRANDAU cTTRANDAU = db.CTTRANDAUs.Find(id,idtd);
        //    if (cTTRANDAU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IDDOI = new SelectList(db.DOIs, "IDDOI", "TENDOI", cTTRANDAU.IDDOI);
        //    ViewBag.IDTD = new SelectList(db.TRANDAUs, "IDTD", "THOIGIANBD", cTTRANDAU.IDTD);
        //    return View(cTTRANDAU);
        //}

        //// POST: Admin/CTTRANDAUs1/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "IDDOI,IDTD,KETQUA,TISO,DIEM")] CTTRANDAU cTTRANDAU)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(cTTRANDAU).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IDDOI = new SelectList(db.DOIs, "IDDOI", "TENDOI", cTTRANDAU.IDDOI);
        //    ViewBag.IDTD = new SelectList(db.TRANDAUs, "IDTD", "THOIGIANBD", cTTRANDAU.IDTD);
        //    return View(cTTRANDAU);
        //}

        // GET: Admin/CTTRANDAUs1/Delete/5
        public ActionResult Delete(int? id, int? idtd)
        {
            if (id == null || idtd == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTTRANDAU cTTRANDAU = db.CTTRANDAUs.Find(id, idtd);
            if (cTTRANDAU == null)
            {
                return HttpNotFound();
            }
            return View(cTTRANDAU);
        }

        // POST: Admin/CTTRANDAUs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? idtd)
        {
            CTTRANDAU cTTRANDAU = db.CTTRANDAUs.Find(id, idtd);
            db.CTTRANDAUs.Remove(cTTRANDAU);
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