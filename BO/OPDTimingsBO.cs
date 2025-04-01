using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OPDTimingsBO
    {
        public int? LanguageId { get; set; }
        public int? OPD_id { get; set; }
        public int? id { get; set; }
        public string OPDName { get; set; }
        public string UnitName { get; set; }
        public string UnitId { get; set; }
        public string Week { get; set; }
        public string StartTimeHH { get; set; }
        public string StartTimeMM { get; set; }
        public string StartTimeTT { get; set; }
        public string EndTimeHH { get; set; }
        public string EndTimeMM { get; set; }
        public string EndTimeTT { get; set; }
        public string DepartmentId { get; set; }
        public string IsActive { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public DataTable dtimg { get; set; }
    }
    public class facultyNameForUnitConfig
    {
        public int? LanguageId { get; set; }
        public int? Id { get; set; }
        public string FacultyName { get; set;}
    }
}
