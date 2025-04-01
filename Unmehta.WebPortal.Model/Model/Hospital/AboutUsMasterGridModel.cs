using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class AboutUsMasterGridModel
    {
        public long? Id { get; set; }
        public long? LanguageId { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string AboutUsDescription { get; set; }
        public string HeadingTitle { get; set; }
        public string RightSideHeadingTitle { get; set; }
    }
    public class AboutUsMasterDesignationGridModel
    {
        public long? Id { get; set; }
        public long? AbountUsId { get; set; }

        public string DesignationName { get; set; }

        public string DesName { get; set; }

        public System.Nullable<int> DesignationId { get; set; }

        public string PhotoName { get; set; }

        public string PhotoPath { get; set; }

        public string Message { get; set; }
    }
}
