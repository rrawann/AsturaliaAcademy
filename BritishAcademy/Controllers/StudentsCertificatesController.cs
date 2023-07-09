using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using BritishAcademy.Data;
using BritishAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BritishAcademy.Controllers
{
    public class StudentsCertificatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsCertificatesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string UserName )
        {
            
            var data = _context.CertficateUploads.AsQueryable();
            var Qr = data.Select(s => new showModel
            {
                ID = s.Id,
                Course = s.Course.Title,
                UserName = s.ApplicationUser.UserName,
                Student = s.ApplicationUser.Name,
                Certificate = s.Name,
                Description = s.Description,
            });

            if (!string.IsNullOrEmpty(UserName))
            {
                Qr = Qr.Where(x => x.UserName == UserName);
             
                TempData["Message"] = UserName;
                ViewBag.Message = TempData["Message"];
            }
 
         
            return View(Qr.OrderBy(x => x.ID));

        }

        public async Task<IActionResult> DownloadFileFromDatabase(int id)
        {

            var file = await _context.CertficateUploads.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;
            return File(file.Data, file.FileType, file.Name + file.Extension);
        }
    }
}