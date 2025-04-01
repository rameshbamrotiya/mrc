using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BannerMasterBO
    {
        public string banner_desc { get; set; }
        public int? banner_id { get; set; }
        public int? IsExist { get; set; }
        public string banner_title { get; set; }
        public string banner_url { get; set; }
        public int? user_id { get; set; }

        public string ip_add { get; set; }
        public string IsActive { get; set; }
        public int banner_rank { get; set; }

    }
}
