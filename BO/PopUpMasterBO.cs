using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class PopUpMasterBO
    {
        public string page_name;
        public string Details { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public string IsActive { get; set; }
    }
}
