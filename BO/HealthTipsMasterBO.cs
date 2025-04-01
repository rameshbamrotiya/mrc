using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class HealthTipsMasterBO
    {
        public int? Health_id { get; set; }
        public string Title { get; set; }
        public string InnerDescription { get; set; }
        public string ShortDesc { get; set; }
        public string Imgpath { get; set; }
        public string Date { get; set; }
        public string InnerImgpath { get; set; }
        public string ReffredBy { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public int? Health_level_id { get; set; }
    }
}
