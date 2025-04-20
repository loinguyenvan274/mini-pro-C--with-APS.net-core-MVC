using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Web_1.DAO;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });


        // Đăng ký dịch vụ Session
        builder.Services.AddDistributedMemoryCache(); // Dùng bộ nhớ cho Session
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20); // Thời gian hết hạn sau khi không sử dụng
            options.Cookie.HttpOnly = true; // Cookie chỉ có thể truy cập từ server
            options.Cookie.IsEssential = true; // Cookie là thiết yếu
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession(); 
        
        app.UseMiddleware<CheckQuyen>();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Login}/{action=Index}/{id?}");
        app.Run();

    }
}