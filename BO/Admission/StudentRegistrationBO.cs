using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Admission
{
    public class StudentRegistrationBO
    {

        public long Id{ get; set; }

        public string NamePrefix{ get; set; }

        public string FirstName{ get; set; }

        public string AadharCard { get; set; }

        public string MiddleName{ get; set; }

        public string LastName{ get; set; }

        public System.Nullable<System.DateTime> DateOfBirth{ get; set; }

        public string Email{ get; set; }

        public string Mobile{ get; set; }

        public string Username{ get; set; }

        public string Password{ get; set; }

        public string Gender{ get; set; }

        public string MaritalStatus{ get; set; }

        public bool IsUsernameReset { get; set; }

        public bool IsDelete{ get; set; }

        public string CreateBy{ get; set; }

        public System.Nullable<System.DateTime> CreateDate{ get; set; }

        public string UpdateBy{ get; set; }

        public System.Nullable<System.DateTime> UpdateDate{ get; set; }

        public string DeleteBy{ get; set; }

        public System.Nullable<System.DateTime> DeleteDate{ get; set; }
    }
    public class EmailOTPBO
    {
        public string MobileNo { get; set; }
        public string otp { get; set; }
    }
}
