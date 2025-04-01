using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ArticlesMasterBO
    {
        public int? Articles_id { get; set; }
        public string Articles_Name { get; set; }
        public string Author { get; set; }
        public string Web_link { get; set; }
        public string Publication_Year { get; set; }
        public string AD_id { get; set; }
        public string AT_id { get; set; }
        public string PT_id { get; set; }
        public string Description { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
