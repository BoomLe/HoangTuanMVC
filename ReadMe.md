## Controller
- là một lớp kế thừa từ controller : Microsoft.AspNet.Mvc.Controller
- Action trong controller là phương thức public (không được Static)
-Action trả về bắt cứ giữ liệu nào, thường là IActionResult
- Các dịch vụ Inject vào Controller qua hàm tạo

## View 
- là File (.cshtml)
- View cho Action lưu lại tại : /View/ControllerName/ACtionName/.cshtml
- Thêm thư mục lưu trữ View:
-  //dường dẫn dưới đây đi theo :
       // {0} là Action
       // {1} là Controller
       // {2} là Areas
       // lúc này hệ thống ưu tiên tìm File như dưới
       option.ViewLocationFormats.Add("/MyViews/{1}/{0}" + RazorViewEngine.ViewExtension);
       // RazorViewEngine.ViewExtension là => đuôi file .cshtml

## Truyền dữ liệu sang View
- Model
- ViewData
- ViewBag
- TempData