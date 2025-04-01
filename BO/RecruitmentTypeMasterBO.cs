using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class RecruitmentTypeMasterBO
    {
        public int? id { get; set; }
        public bool? isWalkin { get; set; }
        public string Recruitment_Name { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
