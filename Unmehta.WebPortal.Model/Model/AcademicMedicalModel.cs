using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{

    public class AcademicMedicalModel
    {
        public long Id{ get; set; }

        public long LangId { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string AcademicsName{ get; set; }

        public string AcademicsFullName{ get; set; }

        public string AcademicsDescription{ get; set; }

        public string ImagePath{ get; set; }
    }

    public class AcademicMedicalDoctorModel
    {
        public long Id { get; set; }

        public long AccId { get; set; }

        public long LangId { get; set; }

        public long YearId { get; set; }

        public string StudentName { get; set; }

        public string DegreeHold { get; set; }

        public string Photo { get; set; }
    }
}
