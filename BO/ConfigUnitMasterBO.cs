using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ConfigUnitMasterBO
    {
        public long? Id { get; set; }

        //public long? LangId { get; set; }
        //public long? maxSlot { get; set; }
        public int? IsExist { get; set; }
        public string UnitName { get; set; }
        public int Username { get; set; }
       // public string SloteName { get; set; }

        public System.Nullable<long> SpecilizationId { get; set; }

        //public System.Nullable<int> WeekNo { get; set; }

        //public string StartTimeHour { get; set; }

        //public string StartTimeMin { get; set; }

        //public string StartTimeTT { get; set; }

        //public string EndTimeHour { get; set; }

        //public string EndTimeMin { get; set; }

        //public string EndTimeTT { get; set; }

        public System.Nullable<bool> IsActive { get; set; }
        public System.Nullable<bool> IsDelete { get; set; }
    }
    public class AppoinmentDetaiBO
    {

        public Nullable<long> Id { get; set; }
        public string PatientName { get; set; }
        public string EMail { get; set; }
        public string MobileNo { get; set; }
        public string VisitTypeId { get; set; }
        public string UNMId { get; set; }
        public string ReasonForVisit { get; set; }
        public Nullable<long> UnitId { get; set; }
        public Nullable<long> DoctorId { get; set; }
        public Nullable<long> SlotId { get; set; }
        public string strAppointmentDate { get; set; }
        public System.Nullable<bool> IsActive { get; set; }
        public System.Nullable<bool> IsDelete { get; set; }
        //public string CreateBy { get; set; }
        //public string CreateDate { get; set; }
        //public string UpdateBy { get; set; }
        //public string UpdateDate { get; set; }
        //public string DeleteBy { get; set; }
        //public string DeleteDate { get; set; }
        public string VisitTypeName { get; set; }
        public string SloteName { get; set; }
        public Nullable<long> deptid { get; set; }
        public string DepartmentName { get; set; }
        public string DoctorName { get; set; }
        public string UnitName { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
    }
}
