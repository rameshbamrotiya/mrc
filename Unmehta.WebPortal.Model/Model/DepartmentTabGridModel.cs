using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Model.Model
{
    public class DepartmentTabGridModel: GetAllDepartmentTabListMasterOurExcIdAndLangIdResult
    {
        public bool TabVisable { get; set; }
        public long SwapFromSequanceNo { get; set; }
        public long SwapToSequanceNo { get; set; }
        public string SwapType { get; set; }
    }
}
