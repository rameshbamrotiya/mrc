using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BO
{
    public class SubServiceMasterBO
    {
        public int? LanguageId { get; set; }
        public int? Speciality_id { get; set; }
        public string SubService_Name { get; set; }
        public string OS_id { get; set; }
        public string Is_active { get; set; }
        public string Description { get; set; }
        public string Img_path { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
