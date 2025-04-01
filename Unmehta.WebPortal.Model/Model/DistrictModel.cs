using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    public class DistrictModel
    {
        public int? Id { get; set; }
        public int? StateId { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string CreatedBy { get; set; }
        public string ModifyBy { get; set; }
        public bool? IsDelete { get; set; }
        public string Ip { get; set; }
    }
}
