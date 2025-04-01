using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Common
{
    public class SessionUserModel
    {
        public long Id{ get; set; }

        public long RoleId{ get; set; }

        public long DepartmentId{ get; set; }

        public long DoctorId { get; set; }

        public string FirstName{ get; set; }

        public string LastName{ get; set; }

        public string Email{ get; set; }

        public string UserName{ get; set; }

        public string Designation{ get; set; }

        public string PhoneNo{ get; set; }
        
        public string UserPassword{ get; set; }

        public string PostMinExperiance { get; set; }

        public bool IsActive{ get; set; }
    }
}
