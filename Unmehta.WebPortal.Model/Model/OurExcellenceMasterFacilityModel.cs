using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class OurExcellenceMasterFacilityModel
    {

        public long Id { get; set; }

        public long OurExcId { get; set; }

        public System.Nullable<long> FacilityId { get; set; }

        public string FacilityName { get; set; }
        
        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }
    }
}
