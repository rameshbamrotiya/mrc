using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{

    public partial class DoctorRegisterModel
    {

        public int RoleId { get; set; }
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ChangeBy { get; set; }

    }
    public partial class FrontAppointmentModel
    {

        public long Id{get;set;}
        public string PatientName{get;set;}
        public string EMail{get;set;}
        public string MobileNo{get;set;}
        public System.Nullable<int> VisitTypeId{get;set;}
        public string VisitTypeName{get;set;}
        public string UNMId{get;set;}
        public string ReasonForVisit{get;set;}
        public System.Nullable<long> UnitId{get;set;}
        public string UnitName{get;set;}
        public System.Nullable<long> DoctorId{get;set;}
        public string FacultyName{get;set;}
        public System.Nullable<int> SlotId{get;set;}
        public string SloteName{get;set;}
        public string SloatStartTime{get;set;}
        public string SloatEndTime{get;set;}
        public string strAppointmentDate{get;set;}
        public System.Nullable<System.DateTime> AppointmentDate{get;set;}
        public System.Nullable<int> maxSlot{get;set;}
        public System.Nullable<System.DateTime> EntryDate{get;set;}

    }
}
