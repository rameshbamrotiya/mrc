using System;


namespace BO
{
    public class NursingCareBO
    {
        public int? Language_id { get; set; }
        public int? NursingCare_id { get; set; }
        public string Is_active { get; set; }
        public string MainImgpath { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string added_by { get; set; }
        public string ip_add { get; set; }
        public int? NursingCareDetail_id { get; set; }
    }
}
