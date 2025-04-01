using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Rights
{
    public class RoleMasterModel 
    {
        public long? Id { get; set; }
        public string Rolename { get; set; }
        public bool? IsActive { get; set; }
        public string IPAddress { get; set; }
    }
}
