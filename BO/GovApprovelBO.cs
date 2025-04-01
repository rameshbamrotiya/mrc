using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class GovApprovelBO
    {
        public int? GovApp_id { get; set; }
        public int? GovApp_level_id { get; set; }
        public string CourseName { get; set; }
        public string Doc_path { get; set; }
        public int Year_id { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
