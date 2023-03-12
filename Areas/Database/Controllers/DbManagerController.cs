using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using App.Data;
using Microsoft.AspNetCore.Identity;

namespace App.Areas.Database.controllers
{
    [Area("Database")]
    [Route("/database-manger/[action]")]
    public class DbManagerController : Controller
    {
        private readonly HoangTuanDB _hoangTuanDb;
         private readonly  UserManager<MyHoangTuan> _useManager;

         private readonly  RoleManager<IdentityRole> _roleManager;


      

        public DbManagerController( HoangTuanDB hoangTuanDb, UserManager<MyHoangTuan> useManager,RoleManager<IdentityRole> roleManager )
        {
            _hoangTuanDb = hoangTuanDb;
           _useManager = useManager;
           _roleManager = roleManager;
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

        public async Task<IActionResult> AdminsAsync()
        {
            var roles = typeof(RoleName).GetFields().ToList();
            foreach (var role in roles)
            {
                var readRole = (string)role.GetRawConstantValue();
                var foundrole =  await _roleManager.FindByNameAsync(readRole);
                if(foundrole ==null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(readRole));
                }
                
            }

            var roleName = await _useManager.FindByNameAsync("HoangTuan");
            if(roleName == null)
            {
                var AdManager = new MyHoangTuan()
                {
                    UserName = "HoangTuan",
                    Email = "hoangtuanboom42@gmail.com",
                    EmailConfirmed = true
                };
                await _useManager.CreateAsync(AdManager, "Tuan@123");
                //////////////////////////////////////////////////Administrator
                await _useManager.AddToRoleAsync(AdManager , RoleName.Administrator);
            }

            StatusMessage = "Cấp Quyền Manager được thi hành";
            return RedirectToAction("Index");


        }
    }
}