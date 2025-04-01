using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class RighttoInformationMasterBO
    {
        public int? RIID { get; set; }
        public string Type { get; set; }
        public int? RIMID { get; set; }
        public int? RIDID { get; set; }
        public string Description { get; set; }
        public string modify_by { get; set; }
        public DateTime? modify_date { get; set; }
        public string ip_add { get; set; }
        public int LanguageID { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool enabled { get; set; }
        public string DocTitle { get; set; }
        public string DocURL { get; set; }
    }
}
