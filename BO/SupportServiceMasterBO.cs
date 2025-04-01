using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class SupportServiceMasterBO
    {
        public int Language { get; set; }
        public string SSName { get; set; }
        public string SSImg { get; set; }
        public int? SS_level_id { get; set; }
        public string SSIcon { get; set; }
        public Boolean enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int SSId { get; set; }
        public int recid { get; set; }
    }
}
