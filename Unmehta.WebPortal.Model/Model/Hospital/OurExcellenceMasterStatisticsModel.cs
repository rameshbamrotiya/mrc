using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class OurExcellenceMasterStatisticsModel
    {
        public long Id{ get; set; }

        public long OurExcId{ get; set; }

        public System.Nullable<long> StatisticsId{ get; set; }

        public string StatisticsName{ get; set; }

        public System.Nullable<long> StatisticsType{ get; set; }

        public string StatisticsTypeName{ get; set; }

        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }

    }
}
