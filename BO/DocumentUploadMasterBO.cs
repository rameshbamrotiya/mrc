using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DocumentUploadMasterBO
    {
        public int? Doc_id { get; set; }
        public string Doc_Name { get; set; }
        public bool? enabled { get; set; }
        //public int? IsExist { get; set; }
        public string Doc_Path { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public string IsActive { get; set; }
    }
}
