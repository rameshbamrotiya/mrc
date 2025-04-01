using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class OurExcellenceMasterFacultyModel
    {
        public long Id { get; set; }

        public long OurExcId { get; set; }

        public System.Nullable<long> FacultyId { get; set; }

        public string FacultyName { get; set; }

        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }
    }
}
