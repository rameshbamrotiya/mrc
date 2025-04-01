using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ConfigUnitSlotDetailMasterBO
    {

        public long? Id { get; set; }
         
        public long maxSlot { get; set; }
        public System.Nullable<int> WeekNo { get; set; }
        public string SloteName { get; set; }

        public System.Nullable<long> SpecilizationId { get; set; }

       

        public string StartTimeHour { get; set; }

        public string StartTimeMin { get; set; }

        public string StartTimeTT { get; set; }

        public string EndTimeHour { get; set; }

        public string EndTimeMin { get; set; }

        public string EndTimeTT { get; set; }
        public int? IsExist { get; set; }
        public System.Nullable<bool> IsActive { get; set; }
        public int Username { get; set; }

        public System.Nullable<bool> IsDelete { get; set; }

    }
    public class ConfigSloteDetailsByidforEdit
    {
        public long? Id { get; set;}
        public long? UnitId { get; set; }
        public long? WeekId { get; set; }
        public string SloteName { get; set; }
        public string WeekName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SunStartTimeHour { get; set; }
        public string SunStartTimeMin { get; set; }
        public string SunStartTimeTT { get; set; }
        public string SunEndTimeHour { get; set; }
        public string SunEndTimeMin { get; set; }
        public string SunEndTimeTT { get; set; }
        public string MaxSlote { get; set; }
        public System.Nullable<bool> IsActive { get; set; }
    }
}
