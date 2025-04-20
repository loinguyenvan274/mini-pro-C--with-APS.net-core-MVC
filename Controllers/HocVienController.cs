using Microsoft.AspNetCore.Mvc;
using Web_1.DAO;
using Web_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Web_1.Controllers.HocVien
{
    public class HocVienController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HocVienController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private string getViewPath(string viewName)
        {
            string root = "~/Views/HocVien/";
            string path = root + viewName + ".cshtml";
            return path;
        }
        public IActionResult Index()
        {
            return View(getViewPath("Home"));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("~/Views/Login/Index.cshtml");
        }

        public IActionResult ViewHoSo()
        {

            return View(getViewPath("Home"));
        }

        public IActionResult XemKhoaHoc()
        {
            ViewBag.HocVien = GetHocVien();
            return View(_dbContext.KhoaHocs.ToList());
        }

        public Models.HocVien? GetHocVien()
        {
            var accountIdString = HttpContext.Session.GetString("AccountId");
            if (string.IsNullOrEmpty(accountIdString))
            {
                return null;
            }

            int id = int.Parse(accountIdString);
            return _dbContext.GetHocVienById(id);
        }


        public IActionResult DangKy(int idKhoaHoc)
        {
            var hocVien = GetHocVien(); // có thể null
            var khoaHoc = _dbContext.KhoaHocs.FirstOrDefault(kh => kh.MaKhoaHoc == idKhoaHoc);

            if (hocVien == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để đăng ký khóa học.";
                return RedirectToAction("Index", "Login");
            }

            if (khoaHoc == null)
            {
                TempData["Error"] = "Khóa học không tồn tại.";
                return RedirectToAction("Index", "KhoaHoc");
            }

            // Kiểm tra xem đã đăng ký chưa
            if (!hocVien.KhoaHocs.Contains(khoaHoc))
            {
                hocVien.KhoaHocs.Add(khoaHoc);
                _dbContext.SaveChanges();
            }

            TempData["Success"] = "Đăng ký thành công!";

            return RedirectToAction("XemKhoaHoc", "HocVien");
        }

        public IActionResult XemKhoaHocDaDangKy()
        {
            var hocVien = GetHocVien(); // có thể null
            if (hocVien == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để xem khóa học đã đăng ký.";
                return RedirectToAction("Index", "Login");
            }

            var khoaHocsDaDangKy = hocVien.KhoaHocs.ToList();
            return View(getViewPath("KhoaHocDK"), khoaHocsDaDangKy);
        }
        public IActionResult HuyDangKy(int maKhoaHoc)
        {
            var hocVien = GetHocVien(); // có thể null

            if (hocVien == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để hủy đăng ký khóa học.";
                return RedirectToAction("Index", "Login");
            }

            var khoaHoc = new KhoaHoc { MaKhoaHoc = maKhoaHoc };
            // Kiểm tra xem đã đăng ký chưa
            if (hocVien.KhoaHocs.Contains(khoaHoc))
            {
                hocVien.KhoaHocs.Remove(khoaHoc);
                _dbContext.SaveChanges();
            }

            TempData["Success"] = "Hủy đăng ký thành công!";

            return RedirectToAction("XemKhoaHocDaDangKy", "HocVien");
        }

        public IActionResult XemHoSo()
        {
            return View(getViewPath("HoSo"), GetHocVien());
        }

        [HttpPost]
        public IActionResult SaveProfile(Models.HocVien hocVien)
        {
            Models.HocVien? currentHocVien = GetHocVien();
            if (currentHocVien == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để cập nhật thông tin.";
                return RedirectToAction("Index", "Login");
            }

            currentHocVien._ten = hocVien._ten;
            currentHocVien._email = hocVien._email;
            currentHocVien._sdt = hocVien._sdt;
            currentHocVien._ngaySinh = hocVien._ngaySinh;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            Models.HocVien? currentHocVien = GetHocVien();
            if (currentHocVien == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để cập nhật thông tin.";
                return RedirectToAction("Index", "Login");
            }
            if (currentHocVien._account.Mk == oldPassword && newPassword == confirmPassword)
            {
                currentHocVien._account.Mk = newPassword;
            }
            _dbContext.SaveChanges();

            TempData["Success"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("XemHoSo", "HocVien");

        }




    }
}