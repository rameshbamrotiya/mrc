using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class ExecutiveLeadershipMasterGridModel
    {
        public int? Id { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Name { get; set; }        
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string PhotoName { get; set; }
        public string PhotoPath { get; set; }
        public string Message { get; set; }
        public bool? IsVisible { get; set; }
    }
}
