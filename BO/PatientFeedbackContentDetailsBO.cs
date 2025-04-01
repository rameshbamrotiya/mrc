using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLib.BO
{
    public class PatientFeedbackContentDetailsBO
    {

        #region Constructor

        public PatientFeedbackContentDetailsBO()
        {
        }

        #endregion Constructors

        public Int32? PFD_ID { get; set; }
        public Int32? PF_ID { get; set; }
        public String PatientFeedback { get; set; }
        public String PatientHospitalGuide { get; set; }
        public Boolean? enabled { get; set; }
        public String added_by { get; set; }
        public DateTime? added_date { get; set; }
        public String modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public String ip_add { get; set; }
        public Int32? LanguageId { get; set; }
        public String MetaDescription { get; set; }
        public String MetaTitle { get; set; }
    }
}
