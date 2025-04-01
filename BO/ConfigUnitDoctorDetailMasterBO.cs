using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ConfigUnitDoctorDetailMasterBO
    {
        public long? Id { get; set; }

        public long? LangId { get; set; }

        public long DoctorId { get; set; }

        public long UnitId { get; set; }
        public int? IsExist { get; set; }
        public long UnitDetailId { get; set; }
        public int Username { get; set; }
        public System.Nullable<bool> IsActive { get; set; }
        public System.Nullable<bool> IsDelete { get; set; }
    }
}
