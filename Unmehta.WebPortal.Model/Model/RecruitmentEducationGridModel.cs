using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class RecruitmentEducationGridModel
    {
        public long? Id { get; set; }
        public string EducationDetailName { get; set; }
        public int EducationType { get; set; }
        public bool? IsVisible { get; set; }
    }
}
