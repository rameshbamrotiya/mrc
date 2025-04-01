using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class HomePageContentBO
    {

        #region Constructor

        public HomePageContentBO()
        {
        }

        #endregion Constructors

        public Int32? Home_ID { get; set; }
        public Boolean? IsActive { get; set; }
        public Int32? AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }
        public Int32? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Boolean? IsDelete { get; set; }
        public String IPAddress { get; set; }
    }
}
