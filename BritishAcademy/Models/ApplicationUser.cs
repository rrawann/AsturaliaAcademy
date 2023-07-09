using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BritishAcademy.Models
{
    //public enum Gender
    //{
       
    //    Male = 1,
    //    Female = 2
    //}

    public class ApplicationUser: IdentityUser
    {
       // [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Name { get; set; }

       //[DataType(DataType.Date)]
       // public DateTime Birthdate { get; set; }
        //[Required]
        //public Gender Gender { get; set; }
       // [Required]
        //[StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //public string Address { get; set; }
       public virtual ICollection<CertficateUpload> CertficateUploads { get; set; }
        
    }
}
