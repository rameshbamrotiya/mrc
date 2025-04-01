using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Admission
{
    public class StudentAdvertisementBO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public System.Nullable<System.DateTime> PublishDate { get; set; }

        public string Desciption { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDelete { get; set; }

        public string CreateBy { get; set; }

        public System.Nullable<System.DateTime> CreateDate { get; set; }

        public string UpdateBy { get; set; }

        public System.Nullable<System.DateTime> UpdateDate { get; set; }

        public string DeleteBy { get; set; }

        public System.Nullable<System.DateTime> DeleteDate { get; set; }
    }
}
