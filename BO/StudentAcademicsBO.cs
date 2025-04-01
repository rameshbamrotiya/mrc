using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StudentAcademicsBO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int EducationId { get; set; }
        public int EducationTypeId { get; set; }
        public int RegisrationId { get; set; }
        public string NameOfSchoolCollege { get; set; }
        public string BoardUniversity { get; set; }
        public string PassingYear { get; set; }
        public string Stream { get; set; }
        public string PercentageOrPercentile { get; set; }
        public string Division { get; set; }
        public string MarksheetPath { get; set; }
        public int NoOfTrials { get; set; }
        public bool IsVisible { get; set; }
        public string UserName { get; set; }    
        public string PassingMonth { get; set; }
        public bool IsDelete { get; set; }
        public string ComputerDetails { get; set; }
        public bool IsComputerLiteracy { get; set; }
        public int? IsExist { get; set; }


        public int IsNonMandatory { get; set; }

    }
    public class StudentAcademicsDocumentBO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int EducationId { get; set; }
        public int EducationTypeId { get; set; }
        public int RegisrationId { get; set; }
        public string DocName { get; set; }
        public string UserName { get; set; }
        public bool IsDelete { get; set; }
        public string DocPath { get; set; }
        public int? IsExist { get; set; }

    }
}
 
       
        