using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model.Hospital
{
    public class BlogCategoryMasterGridModel
    {
        public int? Id { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string BlogName { get; set; }  
        public string Blogger { get; set; }
        public DateTime BlogDate { get; set; }
        public bool? IsVisible { get; set; }
        public int TotalVacancy { get; set; }
        public string TypeDetail { get; set; }
        public bool? IsNewIcon { get; set; }
    }
}
