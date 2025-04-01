using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class OurExcellenceMasterGridModel
    {
        public long? Id { get; set; }
        public long? LanguageId { get; set; }
        public long? DepartmentId { get; set; }
        public string DepartmentName { get; set; }        
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string ImageName { get; set; }
        public string FileFullPath { get; set; }
        public string AddImage { get; set; }
        public string SideImageURL { get; set; }
        public string ExternalVideoLink { get; set; }
        public long SequanceNo { get; set; }
        public long SwapFromSequanceNo { get; set; }
        public long SwapToSequanceNo { get; set; }
        public string SwapType { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsFacility { get; set; }
        public bool? IsAddInOtherDepartment { get; set; }
    }
}
