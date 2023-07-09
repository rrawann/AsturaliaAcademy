using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BritishAcademy.Data;
using BritishAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BritishAcademy.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class SupperAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public SupperAdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateSupperAdmin()
        {
            var user = new ApplicationUser { UserName = "British_Academy", Email = "rawan@yahoo.com", Name = "BritishAcademy", };

            var result = await _userManager.CreateAsync(user, "123456v");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Super Admin");
                return Content("Created User successfully");

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Content("Not Created User");


        }
    }
}