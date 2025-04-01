using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
  public  class FloorMasterBO
    {
        public int LanguageId { get; set; }
        public string FloorName { get; set; }        
        public int Id { get; set; }
        public int FloorId { get; set; }
        public bool Enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public int Floor_level_id { get; set; }
    }
}
