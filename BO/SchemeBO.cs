using System;

namespace BO
{
    public class SchemeBO
    {
        public int Language { get; set; }
        public string SchemeName { get; set; }
        public string SchemeLogo { get; set; }
        public int? scheme_level_id { get; set; }
        public string SchemeBanner { get; set; }
        public string ContactPerson { get; set; }
        public string Location { get; set; }
        public string HelpDeskNo { get; set; }
        public string WebsiteUrl { get; set; }

        //public long? ChartId { get; set; }

        public Boolean enabled { get; set; }
        public string added_by { get; set; }
        public string modified_by { get; set; }
        public string ip_add { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int SchemeId { get; set; }
        public int recid { get; set; }
    }
}
