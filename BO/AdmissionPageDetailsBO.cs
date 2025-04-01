using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AdmissionPageDetailsBO
    {
        public int? AdmissionPageId { get; set; }
        public int? Admission_level_id { get; set; }
        public string CourseName { get; set; }
        public string YearOfAdmission { get; set; }
        public string AdmissionFileName { get; set; }
        public string AdmissionFilePath { get; set; }
        public int? Language_id { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public string user_id { get; set; }
        public string ip_add { get; set; }
    }
}
