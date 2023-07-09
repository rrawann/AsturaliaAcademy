using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BritishAcademy.Models
{
    public class DepUser
    {

        public int Id { get; set; }

      
        //public int DepartmentID { get; set; }
        //public virtual Department Department { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
       



    }
}
