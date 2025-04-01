using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class PatientBrochuerModel
    {
        public long Id{ get; set; }
        
        public long SequanceNo { get; set; }

        public long SwapFromSequanceNo { get; set; }

        public long SwapToSequanceNo { get; set; }

        public string SwapType { get; set; }
        
        public long LangId{ get; set; }

        public string Name{ get; set; }

        public string FrontImage{ get; set; }

        public string FrontImagePath{ get; set; }

        public string Pdf{ get; set; }

        public string PdfPath{ get; set; }

        public bool IsAvailable{ get; set; }
    }
}
