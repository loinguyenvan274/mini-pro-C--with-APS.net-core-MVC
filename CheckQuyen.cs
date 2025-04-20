public class CheckQuyen
{
    private readonly RequestDelegate _next;
    

    public CheckQuyen(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;

        if (!path.StartsWithSegments("/Login") &&
            !path.StartsWithSegments("/Signup") )
        {
            var role = context.Session.GetString("role");
            if(path.StartsWithSegments("/QuanLy") && role != "admin")
            {
                context.Response.Redirect("/Login");
                return;
            }
            if (path.StartsWithSegments("/HocVien") && role != "user")
            {
                context.Response.Redirect("/Login");
                return;
            }

        }

        await _next(context);
    }
}
