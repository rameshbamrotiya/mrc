using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
   public class PageAdminModel
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string HeaderImage { get; set; }
        public string HeaderFullPathImage { get; set; }

        public int ParentId { get; set; }
        public int MenuRank { get; set; }
        public Boolean IsVisible { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string IPAdd { get; set; }
        //public int user_type_id { get; set; }

        public string TemplateId { get; set; }
        public string Tooltip { get; set; }
        public string LanguageId { get; set; }
        public string MaskingURL { get; set; }

        public string ContentDetails { get; set; }
        public int MenuLevel { get; set; }
        public string MenuType { get; set; }
        public string MenuId { get; set; }
        public int id { get; set; }
    }
}
