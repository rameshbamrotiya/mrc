using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
  public  class BlockBO
    {
        public int LanguageId { get; set; }
        public string BlockName { get; set; }        
        public int Recid { get; set; }
        public int BlockID { get; set; }
        public bool Enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public int Block_level_id { get; set; }
    }
}
