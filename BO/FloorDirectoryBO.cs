using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class FloorDirectoryBO
    {
        public int LanguageId { get; set; }
        public int @FloorDirectoryId { get; set; }
        public int? FloorId { get; set; }
        public string CellValue { get; set; }
        public string ToolTip { get; set; }
        public int BlockID { get; set; }
        public int Recid { get; set; }
        public bool Enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }

    }
}
