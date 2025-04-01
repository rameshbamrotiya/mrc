using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BO
{
    public class VideoGalleryMasterBO
    {
        public int? LanguageId { get; set; }
        public int? Video_id { get; set; }
        public int? VideoCategoryid { get; set; }
        public string Video_Name { get; set; }
        public int? Video_level_id { get; set; }
        public string Video_desc { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string Is_active_Video { get; set; }
        public string Link_Video_Upload { get; set; }
        public string Video_Path { get; set; }
        public string Thumbnill_Path { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string Department_id { get; set; }
        public string ip_add { get; set; }
        public string Is_download { get; set; }
        public string TagList { get; set; }
        public DataTable dtimg { get; set; }
    }
}
