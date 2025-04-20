using Web_1.Models;
using Microsoft.AspNetCore.Mvc;
using Web_1.DAO;

namespace Web_1.Controllers
{
    public class SignupController : Controller
    {

        private readonly AppDbContext _dbContext;

        public SignupController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(string name, string email, string phone, DateTime ngaySinh, string username, string password)
        {
            Web_1.Models.HocVien hocVien = new Web_1.Models.HocVien
            {
                _ten = name,
                _email = email,
                _sdt = phone,
                _ngaySinh = ngaySinh,
                _account = new Account
                {
                    TK = username,
                    Mk = password,
                }
            };
            hocVien._account.hocVien = hocVien; // Thiết lập lại quan hệ 2 chiều giữa Account và HocVien
            try
            {
                _dbContext.HocViens.Add(hocVien);
                _dbContext.SaveChanges();
            }catch(Exception ex)
            {
                return BadRequest(new{
                    message = "Đã xảy ra lỗi khi tạo tài khoản.",
                    error = ex.Message
                });
            }
             

            HttpContext.Session.SetString("AccountId", hocVien._account.AccountId.ToString());
            HttpContext.Session.SetString("role", "user");
            return Ok();
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