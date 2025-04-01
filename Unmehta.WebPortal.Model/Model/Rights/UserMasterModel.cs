using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Rights
{
    public class UserMasterModel
    {
        public long? Id { get; set; }
        public long? RoleId { get; set; }
        public string FirstName { get; set; }
        public int? IsExist { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phoneno { get; set; }
        public bool IsActive { get; set; }

    }
}
