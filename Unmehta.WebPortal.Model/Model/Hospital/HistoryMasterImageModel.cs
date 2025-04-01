using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class HistoryMasterImageModel
    {
        public long Id { get; set; }

        public long HistoryId { get; set; }

        public long LanguageId { get; set; }

        public string ImageName { get; set; }

        public string FileFullPath { get; set; }
    }
}
