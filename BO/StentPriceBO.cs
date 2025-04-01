using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StentPriceBO
    {
        public int? StentPrice_id { get; set; }
        public string StentPrice_Type { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
    public class StentPriceTypeSubMasterBO
    {
        public int? StentPriceSub_id { get; set; }
        public int? StentPriceType_id { get; set; }
        public string NAMEOFMANUFACTURINGCOMPANY { get; set; }
        public string BRANDNAME { get; set; }
        public string COSTOFCORONARYSTENT { get; set; }
        public string NAMEOFDISTRIBUTOR { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
