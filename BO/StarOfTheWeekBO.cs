using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StarOfTheWeekBO
    {
        public int? S_id { get; set; }
        public string MetaDescription { get; set; }
        public string Metatitle { get; set; }
        public string Imgpath { get; set; }
        public string StarOfThe { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public string Week { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
