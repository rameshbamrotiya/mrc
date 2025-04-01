using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class EMCSBO
    {
        public int? EId { get; set; }
        public string EMCSName { get; set; }
        public string EMCSLevelId { get; set; }
        public string EMCSDescription { get; set; }
        public int? LanguageId { get; set; }
        public string StatisticsId { get; set; }
        public string Is_active { get; set; }
        public string IsStatistics { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
