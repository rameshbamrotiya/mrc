using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class FacultyGridModel
    {
        public int? Id { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string FacultyName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public decimal? sequenceNo { get; set; }
        public string FacultyDescription { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool? IsVisible { get; set; }
    }
}
