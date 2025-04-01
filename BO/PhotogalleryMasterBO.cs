using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BO
{
    public class PhotogalleryMasterBO
    {
        public int? LanguageId { get; set; }
        public int? Album_id { get; set; }
        public int? Album_level_id { get; set; }
        public int? Img_id { get; set; }
        public string Album_Name { get; set; }
        public string Album_desc { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string Is_active_album { get; set; }
        public string Is_active_img { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string Department_id { get; set; }
        public string ip_add { get; set; }
        public string TagList { get; set; }
        public DataTable dtimg { get; set; }
    }
}
