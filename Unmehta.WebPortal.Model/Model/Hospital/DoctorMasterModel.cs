using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class DoctorMasterModel
    {
        public long Id{ get; set; }

        public long LangId{ get; set; }

        public long SequanceNo{ get; set; }

        public long SwapFromSequanceNo{ get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }

        public string DoctorFirstName{ get; set; }

        public string DoctorMiddleName{ get; set; }

        public string DoctorLastName{ get; set; }

        public string DoctorShotDescription{ get; set; }

        public string DoctorDescription{ get; set; }

        public string DoctorProfilePic{ get; set; }

        public System.Nullable<bool> IsActive{ get; set; }
    }
}
