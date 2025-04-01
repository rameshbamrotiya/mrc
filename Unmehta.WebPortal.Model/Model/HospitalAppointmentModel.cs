using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class HospitalAppointmentModel
    {
        public long Id{ get; set; }

        public long SpecialId { get; set; }

        public string FirstName{ get; set; }

        public string MiddleName{ get; set; }

        public string LastName{ get; set; }

        public System.Nullable<int> Gender{ get; set; }

        public string MobileNumber{ get; set; }

        public string EmailId{ get; set; }

        public System.Nullable<System.DateTime> AppointmentDate{ get; set; }

        public string AppointmentTime{ get; set; }

        public string ReasonForVisit{ get; set; }

        public string AdditionalInformation{ get; set; }

        public bool IsFollowUp{ get; set; }
    }
}
