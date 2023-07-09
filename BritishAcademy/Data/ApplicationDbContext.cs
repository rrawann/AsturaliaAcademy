using System;
using System.Collections.Generic;
using System.Text;
using BritishAcademy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BritishAcademy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
               public DbSet<Course> Courses { get; set; }
      //  public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Role> Role { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<DepUser> DepUsers { get; set; }
      //  public DbSet<FileOnFileSystemModel> FileOnFileSystemModels { get; set; }
       // public DbSet<FileOnDatabaseModel> fileOnDatabaseModels { get; set; }
      public DbSet<CertficateUpload> CertficateUploads { get; set; }
       // public DbSet<CV> CVs { get; set; }
    }
    
}
