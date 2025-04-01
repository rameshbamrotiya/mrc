using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CovidCareMasterBO
    {
        public int? CovidCare_Id { get; set; }
        public int? Language_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUploadPath { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int? Left_Link_Video_Upload { get; set; }
        public string LeftVideoPath { get; set; }
        public string LeftVideoThumbnailPath { get; set; }
        public int? Right_Link_Video_Upload { get; set; }
        public string RightVideoPath { get; set; }
        public string RightVideoThumbnailPath { get; set; }
        public string FAQsTitle { get; set; }
        public string FAQsImageUploadPath { get; set; }
        public string FAQsAccreditationTitle { get; set; }
        public string Is_active { get; set; }        
        public string added_by { get; set; }
        public string ip_add { get; set; }
        public int? IsExist { get; set; }
    }

    public class CovidCareAccredationDetailsBO
    {
        public int? Accredation_Id { get; set; }
        public int? CovidCareDetails_Id { get; set; }
        public int? Language_id { get; set; }
        public string AccredationSubTitle { get; set; }
        public string AccredationDescription { get; set; }
        public string Is_active { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
        public int? Accredation_level_id { get; set; }
    }
}
