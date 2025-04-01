using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    //public class UnitDoctorMasterModel
    //{
    //    public long Id { get; set; }

    //    public long LangId { get; set; }

    //    public long DoctorId { get; set; }

    //    public long UnitId { get; set; }

    //    public long UnitDetailId { get; set; }
    //}

    public class ConfigUnitMasterModel
    {
        public long Id { get; set; }

        public long SpecilizationId { get; set; }

        public string UnitName { get; set; }

        public long UnitId { get; set; }

        public long UnitDetailId { get; set; }
    }

    public class ConfigUnitDoctorDetailMasterModel
    {
        public long Id { get; set; }

        public long UnitId { get; set; }

        public string DoctorId { get; set; } 
         
    }


    public class ConfigUnitSlotDetailMasterModel
    {

        public long Id { get; set; }

        public long UnitId { get; set; } 

        public string SloteName { get; set; }

        public System.Nullable<long> SpecilizationId { get; set; }

        public System.Nullable<int> WeekNo { get; set; }

        public string StartTimeHour { get; set; }

        public string StartTimeMin { get; set; }

        public string StartTimeTT { get; set; }

        public string EndTimeHour { get; set; }

        public string EndTimeMin { get; set; }

        public string EndTimeTT { get; set; }

        public System.Nullable<bool> IsActive { get; set; }

    }
    public class ConfigurUnitModel
    {

        public long Id { get; set; }

        public long LangId { get; set; }

        public string UnitName { get; set; }

        public System.Nullable<long> SpecilizationId { get; set; }

        public System.Nullable<int> WeekNo { get; set; }

        public string StartTimeHour { get; set; }

        public string StartTimeMin { get; set; }

        public string StartTimeTT { get; set; }

        public string EndTimeHour { get; set; }

        public string EndTimeMin { get; set; }

        public string EndTimeTT { get; set; }

        public System.Nullable<bool> IsActive { get; set; }

    }
}
