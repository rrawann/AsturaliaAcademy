using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BritishAcademy.Models
{
    public enum Status
    {

        Single = 1,
        Married = 2
    }
    public class CV
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }


        public Status Status { get; set; }

        public string Address { get; set; }

        public string Educations { get; set; }

        public string Phone { get; set; }

       public string Email { get; set; }

        public string Language { get; set; }

        public string Education { get; set; }

        public string Skils { get; set; }

        public string Course { get; set; }







    }
}
