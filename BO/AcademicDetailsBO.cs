using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AcademicDetailsBO
    {
        public int? id { get; set; }
        public bool CanRead { get; set; }
        public bool CanSpeak { get; set; }
        public bool Canwrite { get; set; }
        public string RegisrationId { get; set; }
        public int? CandidateId { get; set; }
        public string Advertisementid { get; set; }
        public string Userid { get; set; }
        public int? LanguageID { get; set; }
    }
}
