using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AccommodationBO
    {
        public int? Acc_id { get; set; }
        public string Name { get; set; }
        public string Doc_path { get; set; }
        public string Description { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
