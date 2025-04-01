using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class PatientCareMasterBO
    {
        public int? PatientCare_id { get; set; }
        public int? LanguageId { get; set; }
        public string TabName { get; set; }
        public int? TabType { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }        
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public int? PatientCare_level_id { get; set; }
    }
    public class PatientCareGeneralDetailsBO
    {
        public int? TabTypeId { get; set; }
        public int? SubTabId { get; set; }
        public int? LanguageId { get; set; }
        public int? GeneralDetailsId { get; set; }
        public string TabDescription { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
    public class PatientCareBrochureDetailsBO
    {
        public int? TabTypeId { get; set; }
        public int? SubTabId { get; set; }
        public int? LanguageId { get; set; }
        public int? BrochureId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string FileUploadPath { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
    public class PatientCareLeftRightContainDetailsBO
    {
        public int? TabTypeId { get; set; }
        public int? SubTabId { get; set; }
        public int? LanguageId { get; set; }
        public int? LeftRightContainId { get; set; }
        public string TabDescription { get; set; }
        public string ImagePath { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
}
