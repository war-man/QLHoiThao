using QLHoiThaoProject.Models;
using QLHoiThaoProject.Models.EFModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLHoiThaoProject.Areas.Admin.Controllers
{
    public class ThongKeBaoCaoController : BaseController
    {
        // GET: Admin/ThongKeBaoCao
        private QLHoiThaoDbContext db = new QLHoiThaoDbContext();
        
        public ActionResult TimHT_ThongKe()
        {
            ViewBag.IDHT = db.HOITHAOs.ToList();
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DanhSachLopDuocGiai(int? id)
        {            
            var danhsachLop = db.DOIs.Where(d => d.IDHT == id).GroupBy(d => d.LOP.TENLOP).Select(l => new DSGiaiTheoLop { TENLOP = l.Key, SOGIAI = l.Count(), DOIS = l.Where(d => d.BILOAI == false && d.IDHT == id) }).ToList().OrderByDescending(d=>d.SOGIAI);
            ViewBag.DANHSACHLOP = danhsachLop;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DanhSachNganhDuocGiai(int? id)
        {            
            var danhsachNganh = db.DOIs.Where(d => d.IDHT == id).GroupBy(d => d.LOP.NGANH.TENNGANH).Select(l => new DSGiaiTheoNganh { TENNGANH = l.Key, SOGIAI = l.Count() }).ToList().OrderByDescending(d => d.SOGIAI);
            ViewBag.DANHSACHNGANH = danhsachNganh;
            return View();
        }
        public ActionResult DanhSachDangKiMonThiDau(int? id)
        {
            var danhsach1 = db.DOIs.Where(d => d.IDHT == id).GroupBy(d => d.MONTHIDAU.TENMTD).Select(l => new DSDangKiTheoMTD { MON = l.Key, SOLUONG = l.Count() }).ToList().OrderByDescending(d => d.SOLUONG);
            ViewBag.DANHSACHDANGKIMON = danhsach1;
            return View();
        }

        public ActionResult DanhSachThanhVienDoiTheoLop(int? id)
        {//id hoithao
            var danhsach1 =
                db.THUOCDOIs.Include(d => d.DOI).Include(d => d.THANHVIENDOI).
                Where(d => d.DOI.IDHT == id).GroupBy(d => d.DOI.LOP.TENLOP).

                Select(l => new DSThanhVienDoiTheoLop
                { TENLOP = l.Key, THANHVIENS = l.Select(ll => ll.THANHVIENDOI) }).ToList();
            ViewBag.DANHSACHTHANHVIENLOP = danhsach1;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DanhSachKinhPhiHoTroTheoMon(int? id)
        {           
            var danhsachKP = db.KINHPHIHOTROes.Include(d => d.MONTHIDAU).Include(d => d.HOITHAO).Where(d => d.IDHT == id).OrderBy(d=>d.MONTHIDAU.TENMTD).GroupBy(d => d.MONTHIDAU.TENMTD).Select(l => new DSKinhPhiHoTro { TenMTD = l.Key, TongGT=l.Sum(k=>k.GIATRI) }).ToList();
            ViewBag.DANHSACHKP = danhsachKP;
            return View();
        }
        

    }
}