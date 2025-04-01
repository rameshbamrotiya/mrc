using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ArticleDepartmentBO
    {
        public int? AD_id { get; set; }
        public string AD_Name { get; set; }
        public string AD_Title { get; set; }
        public string Imgpath { get; set; }
        public string Iconpath { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
