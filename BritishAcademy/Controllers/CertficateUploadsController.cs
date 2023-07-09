using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BritishAcademy.Data;
using BritishAcademy.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using PagedList.Core;
using Microsoft.AspNetCore.Authorization;

namespace BritishAcademy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CertficateUploadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CertficateUploadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CertficateUploads
        public IActionResult Index(int? page)
        {


            var data = _context.CertficateUploads.AsQueryable();
            var data2 = data.Select(s => new showModel
            {
                ID = s.Id,
                Course = s.Course.Title,
                UserName = s.ApplicationUser.UserName,
                Student = s.ApplicationUser.Name,
                Certificate = s.Name,
                Extension = s.Extension,
                FileType = s.FileType,
                Description = s.Description,
                CreatedOn = s.CreatedOn

            });
            return View(data2.OrderBy(x => x.ID).ThenByDescending(s => s.UserName).ToPagedList(page ?? 1, 10)); ;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadToDatabase(List<IFormFile> files, string description , CertficateUpload certficateUpload)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var fileModel = new CertficateUpload
                    {
                        CreatedOn = DateTime.Now,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        Description = description,
                        CourseID = certficateUpload.CourseID,
                        ApplicationUserId = certficateUpload.ApplicationUserId
                    };
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        fileModel.Data = dataStream.ToArray();
                    }
                    _context.CertficateUploads.Add(fileModel);
                    //  _context.Add(certficateUpload);
                    _context.SaveChanges();
                }
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName", certficateUpload.ApplicationUserId);
            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Title", certficateUpload.CourseID);
            TempData["Message"] = "File successfully uploaded to Database";
            return RedirectToAction("Index");
        }

        // GET: CertficateUploads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certficateUpload = await _context.CertficateUploads
                .Include(c => c.ApplicationUser)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certficateUpload == null)
            {
                return NotFound();
            }

            return View(certficateUpload);
        }

        // GET: CertficateUploads/Create
        public IActionResult Create()
        {
           
            ViewData["ApplicationUserId"] = new SelectList(_context.Users.Where(x => x.Email.Equals(null)), "Id", "UserName");

            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Title");
            return View();
        }

        // POST: CertficateUploads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Data,CourseID,ApplicationUserId,Id,Name,FileType,Extension,Description,CreatedOn")] CertficateUpload certficateUpload)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certficateUpload);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", certficateUpload.ApplicationUserId);
            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Id", certficateUpload.CourseID);
            return View(certficateUpload);
        }

        // GET: CertficateUploads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CertficateUploads = await _context.CertficateUploads.FindAsync(id);
            if (CertficateUploads == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users.Where(x => x.PhoneNumber.Contains("7")), "Id", "UserName");

            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Title");
            return View(CertficateUploads);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, List<IFormFile> files, string description, CertficateUpload certficateUpload)
        {
            if (id != certficateUpload.Id)
            {
                return NotFound();
            }
         
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName);
                        var fileModel = new CertficateUpload
                        {
                            CreatedOn = DateTime.UtcNow,
                            FileType = file.ContentType,
                            Extension = extension,
                            Name = fileName,
                            Description = description,
                            CourseID = certficateUpload.CourseID,
                            ApplicationUserId = certficateUpload.ApplicationUserId
                        };
                        using (var dataStream = new MemoryStream())
                        {
                            await file.CopyToAsync(dataStream);
                            fileModel.Data = dataStream.ToArray();
                        }
                        _context.Update(certficateUpload);
                        await _context.SaveChangesAsync();

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertficateUploadsExists(certficateUpload.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName", certficateUpload.ApplicationUserId);
            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Title", certficateUpload.CourseID);
            TempData["Message"] = "File successfully uploaded to Database";
            return RedirectToAction("Index");
        }

        // POST: CertficateUploads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: CertficateUploads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certficateUpload = await _context.CertficateUploads
                .Include(c => c.ApplicationUser)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certficateUpload == null)
            {
                return NotFound();
            }

            return View(certficateUpload);
        }

        // POST: CertficateUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certficateUpload = await _context.CertficateUploads.FindAsync(id);
            _context.CertficateUploads.Remove(certficateUpload);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertficateUploadExists(int id)
        {
            return _context.CertficateUploads.Any(e => e.Id == id);
        }

    

        private bool CertficateUploadsExists(int id)
        {
            return _context.CertficateUploads.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteFileFromDatabase(int id)
        {

            var file = await _context.CertficateUploads.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.CertficateUploads.Remove(file);
            _context.SaveChanges();
            TempData["Message"] = $"Removed {file.Name + file.Extension} successfully from Database.";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DownloadFileFromDatabase(int id)
        {

            var file = await _context.CertficateUploads.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;
            return File(file.Data, file.FileType, file.Name + file.Extension);
        }

    }
}
