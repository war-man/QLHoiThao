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
    public class TRANDAUsController : Controller
    {
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();

        // GET: Admin/TRANDAUs
        public ActionResult Index()
        {
            var tRANDAUs = db.TRANDAUs.Include(t => t.SANTHIDAU).Include(t => t.VONGDAU).OrderBy(t=>t.IDTD);
            return View(tRANDAUs.ToList());
        }

        public ActionResult TimHT()
        {
            ViewBag.IDHT = db.HOITHAOs.ToList();
            return View();
        }

        //int IdMTD=0;
        // GET: Admin/TRANDAUs/Details/5 //
        public ActionResult TimMTD(int? id)
        {
            //ViewBag.IDBANG = db.BANGDAUs.ToList();            
            var TENHT = db.HOITHAOs.Where(h => h.IDHT == id).Select(h => new { TENHT = h.TENHT }).SingleOrDefault();
            ViewData.Add("TenHT", TENHT.TENHT);
            var Mon = db.VONGDAUs.Where(v => v.IDHT == id).Select(v => new { IDMTD = v.MONTHIDAU.IDMTD, TENMTD=v.MONTHIDAU.TENMTD }).Distinct();
            ViewBag.IDMTD = new SelectList(Mon, "IDMTD","TENMTD");

            var VONGDAU = db.VONGDAUs.Where(v => v.IDHT == id);
            ViewBag.VONGDAU = VONGDAU;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TimMTD(int? id, int IDMTD)
        {
            //ViewBag.IDBANG = db.BANGDAUs.ToList();            
            var VONGDAU = db.VONGDAUs.Where(v => v.IDHT == id && v.IDMTD == IDMTD);
            ViewBag.VONGDAU = VONGDAU;

            var Mon = db.VONGDAUs.Where(v => v.IDHT == id).Select(v => new { IDMTD = v.MONTHIDAU.IDMTD, TENMTD = v.MONTHIDAU.TENMTD }).Distinct();
            ViewBag.IDMTD = new SelectList(Mon, "IDMTD", "TENMTD");
            return View();
        }

        public ActionResult TimBang(int id)
        {
            ViewBag.IDBANG = db.DOIs.Where(d => d.IDMTD == id).Distinct().ToList();//db.DOIs.Where(d => d.IDHT == id).Distinct().ToList();

            return View();
        }

        

        // GET: Admin/TRANDAUs/Create
        public ActionResult Create(int? id)
        {
            var IDHT_IDMTD = db.VONGDAUs.Where(v => v.IDVONGDAU == id).Select(v => new { v.IDHT, v.IDMTD }).SingleOrDefault();

            ViewBag.BANGDAU =
                new SelectList(db.DOIs.Include(b => b.BANGDAU).Where(b => b.IDHT == IDHT_IDMTD.IDHT).
                Select(b => new { IDBANG = b.IDBANG, TENBANG = b.BANGDAU.TENBANG }).Distinct(), "IDBANG", "TENBANG");

            ViewBag.DOI = new SelectList(db.DOIs.Where(d=> d.BILOAI == false && d.IDMTD== IDHT_IDMTD.IDMTD).Select(d=>new { IDDOI=d.IDDOI, TENDOI=d.TENDOI+" - "+d.BANGDAU.TENBANG }), "IDDOI", "TENDOI");
            ViewBag.IDSTD = new SelectList(db.SANTHIDAUs, "IDSTD", "TENSTD");
            ViewBag.IDTRONGTAI = new SelectList(db.TRONGTAIs, "IDTRONGTAI", "TENTRONGTAI");
            
            //ViewBag.IDVONGDAU = new SelectList(db.VONGDAUs, "IDVONGDAU", "TENVONGDAU");

            ViewData.Add("TENVONGDAU", db.VONGDAUs.Where(v => v.IDVONGDAU == id).Select(v => v.TENVONGDAU).SingleOrDefault());
            ViewData.Add("IDVONGDAU", db.VONGDAUs.Where(v => v.IDVONGDAU == id).Select(v => v.IDVONGDAU).SingleOrDefault());


            return View();
        }


        public void createcttran(int matran, int madoi1, int madoi2)
        {
            CTTRANDAU ct = new CTTRANDAU();
            ct.IDTD = matran;
            ct.IDDOI = madoi1;
            ct.KETQUA = null;
            ct.TISO = null;
            CTTRANDAU ct1 = new CTTRANDAU();
            ct1.IDTD = matran;
            ct1.IDDOI = madoi2;
            ct1.KETQUA = null;
            ct1.TISO = null;

            db.CTTRANDAUs.Add(ct); 
            //db.SaveChanges();
            db.CTTRANDAUs.Add(ct1);
            db.SaveChanges();
        }

        // POST: Admin/TRANDAUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVONGDAU,IDTRONGTAI,IDSTD,THOIGIANBD,THOIGIANKT,NGAYTHIDAU,TRANGTHAI")] TRANDAU tRANDAU,int text1, int text2)
        {          
            if (ModelState.IsValid)
            {
                db.TRANDAUs.Add(tRANDAU);
                //var idtd= tRANDAU.IDTD;
                //var idvd = tRANDAU.IDVONGDAU;
                //var idstd = tRANDAU.IDSTD;
                //var idtt = tRANDAU.IDTRONGTAI;
                
                createcttran(tRANDAU.IDTD,text1,text2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDSTD = new SelectList(db.SANTHIDAUs, "IDSTD", "TENSTD", tRANDAU.IDSTD);
            ViewBag.IDTRONGTAI = new SelectList(db.TRONGTAIs, "IDTRONGTAI", "TENTRONGTAI",tRANDAU.IDTRONGTAI);
            ViewBag.IDVONGDAU = new SelectList(db.VONGDAUs, "IDVONGDAU", "TENVONGDAU", tRANDAU.IDVONGDAU);
            return View();
        }

        // GET: Admin/TRANDAUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANDAU tRANDAU = db.TRANDAUs.Find(id);
            if (tRANDAU == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSTD = new SelectList(db.SANTHIDAUs, "IDSTD", "TENSTD", tRANDAU.IDSTD);
            ViewBag.IDVONGDAU = new SelectList(db.VONGDAUs, "IDVONGDAU", "TENVONGDAU", tRANDAU.IDVONGDAU);
            ViewBag.IDTRONGTAI = new SelectList(db.TRONGTAIs, "IDTRONGTAI", "TENTRONGTAI", tRANDAU.IDTRONGTAI);
            return View(tRANDAU);
        }

        // POST: Admin/TRANDAUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTD,IDVONGDAU,IDTRONGTAI,IDSTD,THOIGIANBD,THOIGIANKT,NGAYTHIDAU,TRANGTHAI")] TRANDAU tRANDAU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRANDAU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDSTD = new SelectList(db.SANTHIDAUs, "IDSTD", "TENSTD", tRANDAU.IDSTD);
            ViewBag.IDVONGDAU = new SelectList(db.VONGDAUs, "IDVONGDAU", "TENVONGDAU", tRANDAU.IDVONGDAU);
            ViewBag.IDTRONGTAI = new SelectList(db.TRONGTAIs, "IDTRONGTAI", "TENTRONGTAI", tRANDAU.IDTRONGTAI);
            return View(tRANDAU);
        }

        // GET: Admin/TRANDAUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANDAU tRANDAU = db.TRANDAUs.Find(id);
            if (tRANDAU == null)
            {
                return HttpNotFound();
            }
            return View(tRANDAU);
        }

        // POST: Admin/TRANDAUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRANDAU tRANDAU = db.TRANDAUs.Find(id);
            db.TRANDAUs.Remove(tRANDAU);
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
            var TranDau = db.TRANDAUs.Where(ct => ct.VONGDAU.IDVONGDAU == id).OrderBy(ct => ct.NGAYTHIDAU).ToList();

            List<string> listtrongtai = new List<string>();

            ViewBag.TRONGTAI = listtrongtai;
            //tổng số mẩu tin trong bảng trận đấu
            ViewBag.Total = TranDau.Count;

            ViewBag.TRANDAU = TranDau;

            return View();
        }

    }
}
