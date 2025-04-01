using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class FAQMasterBO
    {
        public int? FAQ_Id { get; set; }
        public int? LanguageId { get; set; }
        public string FAQDescription { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Is_active { get; set; }        
        public string added_by { get; set; }
        public string ip_add { get; set; }
        public int? IsExist { get; set; }
    }

    public class FAQAccredationMasterBO
    {
        public int? Accredation_Id { get; set; }
        public int? FAQDetails_Id { get; set; }
        public int? Language_id { get; set; }
        public string AccredationTitle { get; set; }
        public string AccredationDescription { get; set; }
        public string Is_active { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
}
