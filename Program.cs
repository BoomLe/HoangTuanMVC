using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//ADd Database DBcontext:
builder.Services.AddDbContext<HoangTuanDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HoangTuanDB"));
});
//ADD phương thúc tìm đường dẫn File và thư mục theo ý:
builder.Services.Configure<RazorViewEngineOptions>(option =>
{
    /* File Mặc định tự động lấy của hệ thống
       View/Controller/Action.cshtml
       -MyViews/Controller/Action.cshtml
       */

       //dường dẫn dưới đây đi theo :
       // {0} là Action
       // {1} là Controller
       // {2} là Areas
       // lúc này hệ thống ưu tiên tìm File như dưới
       option.ViewLocationFormats.Add("/MyViews/{1}/{0}.cshtml");
});
builder.Services.AddSingleton<ProductService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

app.UseAuthorization();

//URL: /{controller}/{action}/{id?}
//Home/Index
// lưu ý controller với action ko thiết lập
// mặt dịnh trả về Home website
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();
app.Run();
