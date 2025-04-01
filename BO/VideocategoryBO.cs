using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class VideocategoryBO
    {
        public int LanguageId { get; set; }
        public string VideoCategoryName { get; set; }
        public int Recid { get; set; }
        public int VCID { get; set; }
        public bool Enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public string TagList { get; set; }
        public string ThumbnillPath { get; set; }
    }
}
