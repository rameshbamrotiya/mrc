using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class NewsMasterBO 
    {
        public string Flag { get; set; }
        public int Language_id { get; set; }
        public int news_typeid { get; set; }
        public string news_title { get; set; }
        public string newsBy { get; set; }
        public int? news_id { get; set; }
        public int? IsExist { get; set; }
        public string news_desc { get; set; }
        public DateTime? news_start_date { get; set; }
        public DateTime? news_end_date { get; set; }
        public string DocURL { get; set; }
        public string BannerURL { get; set; }
        public string Location { get; set; }
        public int? user_id { get; set; }
        public string news_type { get; set; }
        public string ip_add { get; set; }
        public string IsActive { get; set; }

        public bool? IsNewIcon { get; set; }
    }
}
