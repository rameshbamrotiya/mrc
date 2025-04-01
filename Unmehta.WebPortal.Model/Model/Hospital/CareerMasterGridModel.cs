using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class CareerMasterGridModel
    {
        public int? Id { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int? TotalVacancy { get; set; }
        public string RequirementType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsVisible { get; set; }
    }
}
