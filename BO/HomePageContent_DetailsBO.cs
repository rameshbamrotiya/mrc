using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class HomePageContent_DetailsBO
    {

        #region Constructor

        public HomePageContent_DetailsBO()
        {
        }

        #endregion Constructors

        public Int32? ID { get; set; }
        public Int32? Home_ID { get; set; }
        public String LeftVideoTitle { get; set; }
        public string link_Video_PathLeft { get; set; }
        public string link_Video_PathRight { get; set; }
        public String LeftVideoURL { get; set; }
        public String LeftImage { get; set; }
        public String RightImage { get; set; }
        public String LeftVideoReadMore { get; set; }
        public String RightVideoTitle { get; set; }
        public String RightVideoURL { get; set; }
        public String RightVideoReadMore { get; set; }
        public Int32? OpdDay { get; set; }
        public Int32? IpdDay { get; set; }
        public Int32? SurgeryDay { get; set; }
        public Int32? ProceduresDay { get; set; }
        public Int32? InvestigationsDay { get; set; }
        public Int32? LanguageID { get; set; }
        public Boolean? IsDelete { get; set; }
        public Boolean? IsActive { get; set; }
        public string AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public String IPAddress { get; set; }

        public String OPDImageIcon { get; set; }
        public String IPDImageIcon { get; set; }
        public String SurgeryImageIcon { get; set; }
        public String ProceduresImageIcon { get; set; }
        public String InvestigationsImageIcon { get; set; }
    }
}
