using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class SMSlogBO
    {
        public int ID { get; set; }
        public string MobileNo { get; set; }
        public string TransectionId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string RequestURL { get; set; }
        public DateTime EntryDatetime { get; set; }
    }
}
