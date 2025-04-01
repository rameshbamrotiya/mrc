using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class FacilitesMasterGridModel
    {
        public int? Id { get; set; }
        public long? LanguageId { get; set; }
        public string FacilitesName { get; set; }
        public bool? Status { get; set; }
        public bool? IsVisible { get; set; }
    }

    public class FacilitesMasterImageGridModel
    {
        public long Id{ get; set; }

        public long LanguageId { get; set; }

        public long FacilitiesId { get; set; }

        public string FacilitesFileInfo{ get; set; }

        public string FacilitesFileName{ get; set; }

        public string FacilitesFilePath { get; set; }
        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }

        public bool IsVisible { get; set; }
    }
}
