using System;


namespace BO
{
    public class ContactUsMasterBO
    {
        public int? Language_id { get; set; }
        public int? ContactUs_Id { get; set; }        
        public string ContactUsDescription { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string Is_active { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
    }
}
