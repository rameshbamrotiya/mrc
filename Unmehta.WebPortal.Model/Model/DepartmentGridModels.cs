using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class DepartmentGridModels
    {
        public long? Id { get; set; }
        public long? DeptId { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string DepartmentName { get; set; }
        public bool? IsVisible { get; set; }
    }
}
