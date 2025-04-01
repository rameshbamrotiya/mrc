using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AdvertisementTypeMasterBO
    {
        public int? id { get; set; }
        public string Advertisement_Type { get; set; }
        public int? AD_level_id { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
