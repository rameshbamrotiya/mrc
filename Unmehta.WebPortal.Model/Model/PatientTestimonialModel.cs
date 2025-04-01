using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class PatientTestimonialModel
    {

        public long Id{ get; set; }

        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }

        public string PatientName{ get; set; }

        public string Description{ get; set; }

        public string ExternalLink { get; set; }
        public string CityName { get; set; }

        public string FilePath{ get; set; }

        public System.Nullable<bool> IsActive{ get; set; }
    }
}
