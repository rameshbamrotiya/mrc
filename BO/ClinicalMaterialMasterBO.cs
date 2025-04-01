using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ClinicalMaterialMasterBO
    {
        public int? CMId { get; set; }
        public string description { get; set; }
        public string metatitle { get; set; }
        public string metadescription { get; set; }
        public int LanguageId { get; set; }
        public bool? IsVisible { get; set; }
        public string user_id { get; set; }
        public string ip_add { get; set; }
    }
}
