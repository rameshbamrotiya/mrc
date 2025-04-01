using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class GoverningBoardMasterModel
    {
        public long Id { get; set; }

        public long LangId { get; set; }

        public string PageDescription { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public System.Nullable<bool> IsActive { get; set; }
    }


    public class GoverningBoardMasterDesignatedDetailsModel
    {
        public long Id { get; set; }

        public long GovBoardId { get; set; }

        public long LangId { get; set; }


        public long DecId { get; set; }

        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }

        public string DesignatedName { get; set; }

        public string DesignationName { get; set; }
        
        public string FilePath { get; set; }

        public System.Nullable<bool> IsActive { get; set; }
    }
}
