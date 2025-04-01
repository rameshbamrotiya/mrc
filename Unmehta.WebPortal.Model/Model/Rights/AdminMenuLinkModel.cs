using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Rights
{
    public class AdminMenuLinkModel
    {

        public long Id{ get; set; }

        public string MenuName{ get; set; }

        public string MenuUrl{ get; set; }

        public int ParentId{ get; set; }

        public System.Nullable<int> MenuRank{ get; set; }

        public bool IsActive{ get; set; }
        public string IPAddress { get; set; }
    }
}
