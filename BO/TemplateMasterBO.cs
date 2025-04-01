using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
  public  class TemplateMasterBO
    {
        
        public string TemplatehtmlContent { get; set; }
        public string ip_add { get; set; }
        public bool? enabled { get; set; }
        public int TemplateId { get; set; }
        public int LanguageId { get; set; }
        public string TemplateName { get; set; }
        public string UserName { get; set; }
    }
}
