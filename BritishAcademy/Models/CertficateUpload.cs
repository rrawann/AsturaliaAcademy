using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BritishAcademy.Models
{
    public class CertficateUpload: CertificateModel
    {
     
        public byte[] Data { get; set; }
        [Display(Name = "Course")]
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
        [Display(Name = "Student")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }



    }
}
