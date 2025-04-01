using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class WetreatBO
    {
        public int? W_id { get; set; }
        public string WetreatDesc { get; set; }
        public string Imgpath { get; set; }
        public string Description { get; set; }
        public int? LanguageId { get; set; }
        public int? DepartmentId { get; set; }
        public string Is_active { get; set; }
        public string Readmore { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
