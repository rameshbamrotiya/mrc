using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLib.BO
{
    public class FacilityInECMSDetailsBO
    {

        #region Constructor

        public FacilityInECMSDetailsBO()
        {
        }

        #endregion Constructors

        public Int32? FIEID { get; set; }
        public Int32? FIEMID { get; set; }
        public Int32? FIEMDID { get; set; }
        public String Title { get; set; }
        public String Subtitle { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
        public Int32? LanguageID { get; set; }
        public Boolean? Enabled { get; set; }
        public String Added_By { get; set; }
        public DateTime? Added_Date { get; set; }
        public String Modify_By { get; set; }
        public DateTime? Modify_Date { get; set; }
        public String IP_Add { get; set; }
        public String MetaDescription { get; set; }
        public String MetaTitle { get; set; }
        public int? IsExist { get; set; }
    }

    public class lstFacilityMaster
    {
        public Int32? FIEMID { get; set; }
        public String Title { get; set; }
    }

    public class lstFacilityMasterDetail
    {
        public Int32? FIEMID { get; set; }
        public String Subtitle { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
    }
}
