using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class HolidayMasterModel
    {
        public int? id { get; set; }
        public DateTime? h_date { get; set; }
        public string h_desc { get; set; }
        public int? IsExist { get; set; }
        public string user_id { get; set; }
        public string ip_add { get; set; }
        public bool IsActive { get; set; }
    }
}
