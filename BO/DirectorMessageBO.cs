using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DirectorMessageBO
    {
        public string DirectorMesshtmlContent { get; set; }
        public string ip_add { get; set; }
        public bool? enabled { get; set; }
        public int DMId { get; set; }
        public int LanguageId { get; set; }
        public string DOCPath { get; set; }
        public string UserName { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
    }
}
