using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Admission
{
    public class StudentEducationDocumentBO
    {
        public long Id { get; set; }

        public int CourseID { get; set; }

        public string CourseName { get; set; }


        public string EducationDetailName { get; set; }

        public int EducationType { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDelete { get; set; }

        public string CreateBy { get; set; }

        public string DocumentName { get; set; }
        public string isrequired { get; set; }

        public System.Nullable<System.DateTime> CreateDate { get; set; }

        public string UpdateBy { get; set; }

        public System.Nullable<System.DateTime> UpdateDate { get; set; }

        public string DeleteBy { get; set; }

        public System.Nullable<System.DateTime> DeleteDate { get; set; }
    }
}
