using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class SpecialityMasterBO
    {
        public int? OS_id { get; set; }
        public int? Img_id { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string ImgTitle { get; set; }
        public string ImgShortDesc { get; set; }
        public string ImgPOPUpDesc { get; set; }
        public string Imgpath { get; set; }
        public string SubImgpath { get; set; }
        public string Iconpath { get; set; }
        public string InnerDesc { get; set; }
        public string IsImg { get; set; }
        public string IsStatistics { get; set; }
        public string InnerImgpath { get; set; }
        public string InnerVideoLink { get; set; }
        public int? OSLevelId { get; set; }
        public DataTable dtimg { get; set; }
        public int? LanguageId { get; set; }
        public int? StatisticsId { get; set; }
        public string Is_active { get; set; }
        public string Is_activeImg { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
