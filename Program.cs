using System.ComponentModel.Design.Serialization;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using App.ExtendMethods;
using App.ExtenMethod;
using App.Data;

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

//Add ẩn menu
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminManager", builders=>{
        builders.RequireAuthenticatedUser();
        builders.RequireRole(RoleName.Administrator);
    });
});

// builder.Services.AddEntityFrameworkSqlServer();

builder.Services.AddDefaultIdentity<MyHoangTuan>(options => options.SignIn.RequireConfirmedAccount = true)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<HoangTuanDB>()
.AddDefaultTokenProviders();




//Add Thư viện cho Login-Out
// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions> (options => {
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

});
//ADD MailKit
//Add thư viện MailSetiing làm việc nhớ cài Maikit và Mimekit
// builder.Configuration.GetSection("MailSetting");
// builder.Services.Configure<MailSettings>((builder.Configuration.GetSection("MailSetting")));

builder.Services.AddSingleton<IEmailSender, SendMailService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login/";
    options.LogoutPath = "/logout/";
    options.AccessDeniedPath = "/Khongtruycap.html";
});

// ADD dịch vụ thứ 3 Google
builder.Services.AddAuthentication().AddGoogle(options =>
{
    var addGoogle = builder.Configuration.GetSection("Authentication:Google");
    options.ClientId = addGoogle["ClientId"];
    options.ClientSecret = addGoogle["ClientSecret"];
    options.CallbackPath ="/dang-nhap-tu-google/";
});


builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<IdentityErrorDescriber ,AppIdentityErrorDescriber>();

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
app.UseCookiePolicy();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.AddErrorfix();

app.UseAuthentication();
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
