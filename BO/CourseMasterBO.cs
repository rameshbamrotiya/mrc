using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BO
{
    public class CourseMasterBO
    {
        public int? LanguageId { get; set; }
        public int? Course_id { get; set; }
        public string Course_Name { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public DataTable dtimg { get; set; }
    }
}
