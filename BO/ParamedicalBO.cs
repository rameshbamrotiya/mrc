using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class ParamedicalBO
    {
        public int languageId { get; set; }
        public string CourseName { get; set; }
        public string coursecode { get; set; }

        public string Totalseats { get; set; }
        public string CourseDuration { get; set; }
        public string description { get; set; }
        public string Fees { get; set; }
        public string col_menu_name { get; set; }
        public Boolean enabled { get; set; }
        public string ImagePath { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string ipadd { get; set; }
        public int courseid { get; set; }
    }
}
