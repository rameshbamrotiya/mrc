using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class PatientFeedbackGridModel
    {
        public int? Id { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string FeedBackDetails { get; set; }
        public bool? IsVisible { get; set; }
    }
}
