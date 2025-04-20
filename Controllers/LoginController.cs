using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_1.Models;
using Web_1.DAO;
#pragma warning restore format

namespace Web_1.Controllers
{
    public class LogInController : Controller
    {

        private readonly AppDbContext _dbContext;
        public LogInController(DbContextOptions<AppDbContext> options)
        {

            _dbContext = new AppDbContext(options);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("login")]
        public IActionResult Login_1(string TaiKhoang, string MatKhau)
        {
            if (TaiKhoang == "1" && MatKhau == "1")
            {
                HttpContext.Session.SetString("AccountId", TaiKhoang);
                HttpContext.Session.SetString("role", "admin");
                return RedirectToAction("Index", "QuanLy");
            }

            Account? account = _dbContext.Accounts.FirstOrDefault(a => a.TK == TaiKhoang && a.Mk == MatKhau);
            if (account != null)
            {
                HttpContext.Session.SetString("AccountId", account.AccountId.ToString());
                HttpContext.Session.SetString("role", "user");
                return RedirectToAction("Index", "HocVien");
            }

            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            return RedirectToAction("Index");
        }
        // [HttpPost]
        // public IActionResult Index(string username, string password)
        // {
        //     // Xử lý đăng nhập ở đây
        //     // Kiểm tra thông tin đăng nhập và chuyển hướng đến trang khác nếu thành công
        //     if (username == "admin" && password == "password")
        //     {
        //         return RedirectToAction("Index", "Home");
        //     }
        //     else
        //     {
        //         ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
        //         return View();
        //     }
        // }
    }
}