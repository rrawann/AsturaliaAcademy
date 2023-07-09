using BritishAcademy.Data;
using BritishAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace BritishAcademy.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RolesController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        // GET: Roles
      
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
      //  [Authorize(Roles = "Super Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      //  [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
                if (!roleExist)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
                }
                //_context.Add(role);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
       // [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public async Task<IActionResult> Edit(IdentityRole role, string id)
        {
            var roleAsp = await _roleManager.Roles
    .FirstOrDefaultAsync(m => m.Id == id);
            if (id != roleAsp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    roleAsp.Name = role.Name;
                    roleAsp.NormalizedName = (role.Name).ToUpper();
                    var result = await _roleManager.UpdateAsync(roleAsp);
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    var roleExist = await _roleManager.RoleExistsAsync(role.Id);
                    if (!roleExist)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            await _roleManager.DeleteAsync(role);
          
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return _context.Role.Any(e => e.Id == id);
        }



      

    }
}
