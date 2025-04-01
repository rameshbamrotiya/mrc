using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AlumniStudentMasterBO
    {
        public int? AlumniStudentId { get; set; }
        public int? LanguageId { get; set; }
        public string Year { get; set; }
        public string Title { get; set; }        
        public string FileUploadPath { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
}
