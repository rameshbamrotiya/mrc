using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class NursingCareAccordionTypeMasterBO
    {
        public int? Accordion_id { get; set; }
        public string Accordion_Name { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
    public class NursingCareAccordionTypeSubMasterBO
    {
        public int? AccordionSub_id { get; set; }
        public int? AccordionType_id { get; set; }
        public string AccordionSubTitle { get; set; }
        public string AccordionSubDescription { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
