//using BC =BCrypt.Net.BCrypt;
using BritishAcademy.Data;
using BritishAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Linq;
using System.Threading.Tasks;


namespace AdminManageAccount.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminManageAccontController : Controller
    {
    
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminManageAccontController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
        
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        

        }
        // GET: AdminManageAcconts
        public IActionResult Index(string UserName, int? page)
        {
           

            var res = _context.Users.Where(x => x.Email.Equals(null));
            var Qr = res.Select(x => new ApplicationUser
            {
                Id = x.Id,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                //Address = x.Address,
                //Gender = x.Gender,
                UserName = x.UserName,
                //Email = x.Email,
                //Birthdate = x.Birthdate

            });

            if (!string.IsNullOrEmpty(UserName))
            {
                Qr = Qr.Where(x => x.UserName == UserName);
            }
        
            return View(Qr.OrderBy(s => s.Id).ThenByDescending(s => s.UserName).ToPagedList(page ?? 1, 10));
           
        }

        // GET: AdminManageAcconts/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _userManager.FindByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: AdminManageAcconts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminManageAcconts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
            

                var s = new ApplicationUser
                {
                    Name = applicationUser.Name,
                    PhoneNumber = applicationUser.PhoneNumber,
                    //Address = applicationUser.Address,
                    //Birthdate = applicationUser.Birthdate,
                    
                    //Email = applicationUser.Email,
             

                    UserName = (applicationUser.Name).Substring(0, 3) + "_" + (applicationUser.PhoneNumber).Substring(1, 5),
             
                    //Gender = applicationUser.Gender
                };
        
                await _userManager.CreateAsync(s, (applicationUser.Name).Substring(0, 3) + "_" + (applicationUser.PhoneNumber).Substring(1, 5));
           
                await _userManager.AddToRoleAsync(s, "Student");
      
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
        }

        // GET: AdminManageAcconts/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _userManager.FindByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: AdminManageAcconts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, ApplicationUser application)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Name = application.Name;
                    user.UserName = application.UserName;
                    //user.Email = application.Email;
                    user.PhoneNumber = application.PhoneNumber;
                    //user.Address = application.Address;
                    //user.Birthdate = application.Birthdate;
                    //user.Gender = application.Gender;


                    user.UserName = (application.Name).Substring(0, 3) + "_" + (application.PhoneNumber).Substring(1, 5);
                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: AdminManageAcconts/Delete/5
        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var account = await _userManager.FindByIdAsync(id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(account);
        //}

        //// POST: AdminManageAcconts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConformed(string id)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here
        //        var account = await _userManager.FindByIdAsync(id);
        //        await _userManager.DeleteAsync(account);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
       


    }
}