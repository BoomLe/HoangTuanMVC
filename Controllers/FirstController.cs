using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace  App.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly IWebHostEnvironment _env;

        private readonly ProductService _productService;
        
        public FirstController(ILogger<FirstController> logger,IWebHostEnvironment env, ProductService productService)
        {
            _logger = logger;
            _env = env;
            
            _productService=productService;

        }
        public string Index()
        {
            /* - this.HttpContext
               - this.Request
               - this.Response
               - this.RouteData

               - this.User
               - this.ModelState
               - this.ViewData
               - this.ViewBag
               - this.Url
               - this.Tempdata         
            */
            _logger.LogInformation("FirstController đang chạy");
            return "Tôi là first Index"; 
        }
// phương thức không hoạt đông Net 7
        // public void Nothing()
        // {
        //     _logger.LogInformation("Nothing hoạt động");
        //     Response.Headers.Add("hi", "Wellcome Home"); 
            
        // }


        public object Anything() => Math.Sqrt(25);




        //ContentResult   | Content()
        //phương thức trả về nội dung trên website
        public IActionResult Readme()
        {
            var content =@"Xin chào các bạn đang học MVC";

            return Content(content);
        }

        //FileResult      | File()
        // phương thức chạy file thư mục một hình ảnh lên website
        public IActionResult Images()
        {
            var filePath = Path.Combine(_env.ContentRootPath, "File", "hoa.PNG");
            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes , "image/png");

        }

        //JsonResult    | Json()
        // phương thức trả về data Json
        public IActionResult IOSPhone()
        {
            return Json(
                new {
                    Name = "Iphone12",
                    Price = 444
                }
            );
        }

        //  LocalRedirectResult     | LocalRedirect()
        // đi tới những trang web nội bộ của hệ thống mà 
        // người đã thiết lập
        // phương thức ở dưới truy cập First/Privacy => trả về 
        // trang Home/Privacy
        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy", "Home");
            _logger.LogInformation("ACtion LocalWhere");
            return LocalRedirect(url);
        }


        // RedirectResult    | Redirect()
        //Phương thức truy cập website ngoài
        public IActionResult Google()
        {
            var url = "https://Google.com";
            _logger.LogInformation("Đang bay tới Google");
            return Redirect(url);
        }

        //ViewResult      | View()
        public IActionResult Hallo(string name)
        {
            // Views => Razor Engine đọc (.cshtml) (template)
            //-----------------------------------------------

            // cách truyền File :
            /* Views(template) -> tempalte đường dẫn tuyệt đối .cshtml trong Return View();
              hoặc truyền Views(template, model)
               return View("/MyViews/test.cshtml" , name);
            */

            /*file: test.cshtml => được tìm trong thư mục /View/First/test.cshtml
               return View ("test, name)
               nếu như ta không thiết lập đường dẫn cụ thể hệ thông sẽ tự động lấy như trên
            */

            /* return View(object(name)); không đưa đường dẫn hoặc file hệ thông tụ đông tìm tên {action}
               đặt mặc dinh là file
               hallo.cshtml => View/First/hallo.cshtml

            */
            if(string.IsNullOrEmpty(name))
            name = "Tuấn";
            return View("/MyViews/test.cshtml" , name);
        }

        public IActionResult Product(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if(product == null)
            {
                TempData ["StatusMessage"] =" Sản phẩm không tồn tại";
                return Redirect(Url.Action("Index", "Home"));
            }

            // HỆ thống mặc định sẽ lấy file như sau :
            // View/First/product.cshtml
            // hay đường dẫn mà ta thiết lập"/MyViews/{1}/{0}.cshtml" => trong Program.cs
            // return View(product);


            //ViewData :
            // truyền dữ liệu từ View(.cshtml) sang => Controller
            // File mặc đinh hệ thống lây:
            // ==> View/First/product.cshtml
            // this.ViewData["product"] = product;
            // this.ViewData["Title"] = product.Name;
            // return View("Product2");


            //ViewBag
            // truyền dữ liệu kiểu ViewBag (.cshtml)
             // File mặc đinh hệ thống lây:
            // ==> View/First/product.cshtml
            ViewBag.product = product;
            return View("Product3");

           

            

        }


    }

    /* -  Kiểu trả về                 | Phương thức
    ------------------------------------------------
    ContentResult               | Content()
    EmptyResult                 | new EmptyResult()
    FileResult                  | File()
    ForbidResult                | Forbid()
    JsonResult                  | Json()
    LocalRedirectResult         | LocalRedirect()
    RedirectResult              | Redirect()
    RedirectToActionResult      | RedirectToAction()
    RedirectToPageResult        | RedirectToRoute()
    RedirectToRouteResult       | RedirectToPage()
    PartialViewResult           | PartialView()
    ViewComponentResult         | ViewComponent()
    StatusCodeResult            | StatusCode()
    ViewResult                  | View()
    */
}