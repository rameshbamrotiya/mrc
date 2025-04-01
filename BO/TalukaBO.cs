using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TalukaBO
    {
        public int? Taluka_id { get; set; }
        public int? City_id { get; set; }
        public int? Country_id { get; set; }
        public int? State_id { get; set; }
        public string Taluka_Name { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
