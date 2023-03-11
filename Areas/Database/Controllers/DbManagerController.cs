using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Database.controllers
{
    [Area("Database")]
    [Route("/database-manger/[action]")]
    public class DbManagerController : Controller
    {
        private readonly HoangTuanDB _hoangTuanDb;

      

        public DbManagerController( HoangTuanDB hoangTuanDb)
        {
            _hoangTuanDb = hoangTuanDb;
        }
        public IActionResult Index()
        {
            return View();
        }


         [HttpGet]
        public async Task<IActionResult> DeleteDb()
        {
            
            return View();
        }


        [TempData]
        public string StatusMessage{set;get;}

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var deleteDb = await _hoangTuanDb.Database.EnsureDeletedAsync();
            StatusMessage = deleteDb ? "Xóa thành công" : "Không thể xóa";

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult>  Created()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatedAsync()
        {
            var created = await _hoangTuanDb.Database.EnsureCreatedAsync();
            StatusMessage = created ? "Tạo thành công" : "Không thể Tạo";
            return RedirectToAction(nameof(Index));
        }
    }
}