using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StudentGenerateMeritNumbersBO
    {
        public long? Id { get; set; }
        public long? StudentId { get; set; }
        public long? CourseId { get; set; }
        public string RegistrationId { get; set; }
        public string FullName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PresentPincode { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string PresentPhoneR { get; set; }
        public string PresentPhoneM { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long? Age { get; set; }
        public string CastName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public long? CasteId { get; set; }
    }
}
