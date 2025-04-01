using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CallUsMasterBO
    {
        public int? CallUs_id { get; set; }
        public string CallUsType_id { get; set; }
        public string Title { get; set; }
        public string Time { get; set; }
        public string Number { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
        public int? CallUsLevelid { get; set; }
    }
}
