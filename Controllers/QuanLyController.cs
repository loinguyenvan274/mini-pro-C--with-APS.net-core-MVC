using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_1.DAO;
using Web_1.Models;

namespace Web_1.Controllers
{
    public class QuanLyController : Controller
    {
        private readonly AppDbContext _dbContext;

        public QuanLyController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private string getViewPath(string viewName)
        {
            string root = "~/Views/QuanLy/";
            string path = root + viewName + ".cshtml";
            return path;
        }
        public IActionResult Index()
        {
            // Create a demo HocVien object and save it to the database
            ViewData["Title"] = "mama";
            return View(getViewPath("Home"));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("~/Views/Login/Index.cshtml");
        }

        [HttpPost]
        public IActionResult CreateKhoaHoc(KhoaHoc khoaHoc)
        {
            if (khoaHoc != null)
            {
                _dbContext.KhoaHocs.Add(khoaHoc);
                _dbContext.SaveChanges();
                return View(getViewPath("Home"));
            }

            return View(getViewPath("TaoKhoaHoc"));
        }

        public IActionResult ViewDanhSachKH()
        {
            return View(getViewPath("DanhSachKhoaHoc"), _dbContext.KhoaHocs.Include(k => k._hocViens).ToList());
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ViewHocVien()
        {
            var hocViens = _dbContext.HocViens.ToList();
            return View(getViewPath("DanhSachHocVien"), hocViens);
        }


        public IActionResult ThongKeThuNhap()
        {

            return View(getViewPath("ThongKeThuNhap"));
        }
        public IActionResult ViewTaoKhoaHoc()
        {
            return View(getViewPath("TaoKhoaHoc"));
        }
        public IActionResult EditKhoaHoc(int maKhoaHoc)
        {
            var khoaHoc = _dbContext.KhoaHocs.FirstOrDefault(kh => kh.MaKhoaHoc == maKhoaHoc);
            if (khoaHoc == null)
            {
                return NotFound();
            }
            return View(getViewPath("SuaKhoaHoc"), khoaHoc);
        }

        [HttpPost]
        public IActionResult EditKhoaHoc(KhoaHoc khoaHoc)
        {
            if (khoaHoc != null)
            {
                _dbContext.KhoaHocs.Update(khoaHoc);
                _dbContext.SaveChanges();
                return RedirectToAction("ViewDanhSachKH");
            }
            return View(getViewPath("SuaKhoaHoc"), khoaHoc);
        }

        public IActionResult XoaKhoaHoc(int maKhoaHoc)
        {
            var khoaHoc = _dbContext.KhoaHocs.FirstOrDefault(kh => kh.MaKhoaHoc == maKhoaHoc);
            if (khoaHoc == null)
            {
                return NotFound();
            }

            _dbContext.KhoaHocs.Remove(khoaHoc);
            _dbContext.SaveChanges();
            return RedirectToAction("ViewDanhSachKH");
        }
        [HttpPost]
        public IActionResult KetQua(DateTime tuNgay, DateTime denNgay)
        {
            var khoaHocs = _dbContext.KhoaHocs.Include(k => k._hocViens)
                .Where(k => k._thoiGianKhaiGiang >= tuNgay && k._thoiGianKhaiGiang <= denNgay)
                .ToList();

            var tongThuNhap = khoaHocs.Sum(k => k._hocPhi * k._soHocVienHienTai);

            ViewBag.TuNgay = tuNgay.ToString("dd/MM/yyyy");
            ViewBag.DenNgay = denNgay.ToString("dd/MM/yyyy");
            ViewBag.TongThuNhap = tongThuNhap;

            return View(getViewPath("ThongKeThuNhap"));
        }
    }
}
