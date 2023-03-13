using System.IO.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Authorization;

namespace App.Areas.Contact.Controllers
{
    [Area("Contact")]
    public class ContactController : Controller
    {
        private readonly HoangTuanDB _context;

        public ContactController(HoangTuanDB context)
        {
            _context = context;
        }

        // GET: Contact
        [HttpGet("/admin/contacts")]
        public async Task<IActionResult> Index()
        {
              return _context.ContactModels != null ? 
                          View(await _context.ContactModels.ToListAsync()) :
                          Problem("Entity set 'HoangTuanDB.ContactModels'  is null.");
        }

        // GET: Contact/Details/5
        [HttpGet("/admin/contacts/details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContactModels == null)
            {
                return NotFound();
            }

            var contactModel = await _context.ContactModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }
        [TempData]
        public string StatusMessage{set;get;}

        // GET: Contact/Create
        [HttpGet("/contact/")]
        [AllowAnonymous]
        public IActionResult SendContact()
        {
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost("/contact/")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendContact([Bind("Id,FullName,Email,DateSend,message,Phone")] ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                contactModel.DateSend = DateTime.Now;
                _context.Add(contactModel);
                await _context.SaveChangesAsync();
                StatusMessage = "Thông tin đã được gủi !";
                return RedirectToAction("Index", "Home");
            }
            return View(contactModel);
        }


        // GET: Contact/Delete/5
        [HttpGet("/admin/contacts/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactModels == null)
            {
                return NotFound();
            }

            var contactModel = await _context.ContactModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        // POST: Contact/Delete/5
        [HttpPost("/admin/contacts/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactModels == null)
            {
                return Problem("Entity set 'HoangTuanDB.ContactModels'  is null.");
            }
            var contactModel = await _context.ContactModels.FindAsync(id);
            if (contactModel != null)
            {
                _context.ContactModels.Remove(contactModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
