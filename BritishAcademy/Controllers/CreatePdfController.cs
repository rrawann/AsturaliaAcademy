using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BritishAcademy.Data;
using BritishAcademy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BritishAcademy.Controllers
{
    public class CreatePdfController : Controller
    {
        private readonly ApplicationDbContext _context;
      

        public CreatePdfController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
           
        }

       

      
        public ActionResult addInfo()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult addInfo(CV element)
        {
            ViewBag.Course = element.Course;
            ViewBag.FullName = element.FullName;
            ViewBag.Address = element.Address;
            ViewBag.Phone = element.Phone;
            ViewBag.Language = element.Language;
            ViewBag.Skils = element.Skils;
            ViewBag.Birthdate = element.Birthdate;
            ViewBag.Status = element.Status;
            ViewBag.Education = element.Education;
            ViewBag.Email = element.Email;

            ViewBag.Educations = element.Educations;

            return View("index");
        }



        public ActionResult CV()
        {

            return View();
        }

    }

}