using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class HistoryMasterModel
    {

        public long Id{ get; set; }

        public long LanguageId{ get; set; }

        public string Year{ get; set; }

        public string HistoryTitle{ get; set; }

        public string HistoryDescription{ get; set; }

        public string HistoryPhotoName { get; set; }
        public string HistoryPhotoPath { get; set; }

        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        

        public string SwapType { get; set; }

        public bool? IsVisible{ get; set; }
    }
}
