using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BritishAcademy.Models
{
   
    public class CvViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        //public string Number { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public dynamic Gender { get; set; }
        public string Address { get; set; }

        public string Courses { get; set; }

    }
}
