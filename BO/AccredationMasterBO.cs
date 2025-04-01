using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BO
{
    public class AccredationMasterBO
    {
        public int? LanguageId { get; set; }
        public int? Acc_id { get; set; }
        public int? AM_id { get; set; }
        public string ImgLogo { get; set; }
        public string Img_Path { get; set; }
        public string Accredation_Name { get; set; }
        public string Accredation_Description { get; set; }
        public string date { get; set; }
        public string MetaDescription { get; set; }
        public string Accredation_Title { get; set; }
        public string MetaTitle { get; set; }
        public string IsVisible { get; set; }
        public string AccredationDesc { get; set; }
        public string AccredationURL { get; set; }
        public string Accredation_MonthYear { get; set; }
        public string Is_active { get; set; }
        public bool IsDisplayInHeader { get; set; }
        public bool IsDisplayInFooter { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
