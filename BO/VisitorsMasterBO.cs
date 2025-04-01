using System.Data;

namespace BO
{
    public class VisitorsMasterBO
    {
        public int? VisitorId { get; set; }
        public int? Img_id { get; set; }
        public string VisitingHoursDesc { get; set; }
        public string DDDescription { get; set; }
        public string MetaTitle { get; set; }
        public string ImgTitle { get; set; }
        public string MetaDescription { get; set; }
        public string ImgPOPUpDesc { get; set; }
        public string Iconpath { get; set; }
        public DataTable dtimg { get; set; }
        public int? LanguageId { get; set; }
        public string Is_active { get; set; }
        public string Is_activeImg { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
