using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace BO
{
    public class GovBodyMasterBO
    {
        public int? LanguageId { get; set; }
        public int? Gov_id { get; set; }
        public string Gov_Name { get; set; }
        public string Gov_desc { get; set; }
        public string Is_active_Video { get; set; }
        public string Gov_Path { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
