using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Rights
{
    public class UserRightsModel
    {
        public int RoleId { get; set; }
        public int ParentId { get; set; }
        public bool CanAdd { get; set; }
        public bool CanView { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public int MenuId { get; set; }
        public string added_by { get; set; }
        public string added_date { get; set; }
        public string ipaddress { get; set; }
    }
}
