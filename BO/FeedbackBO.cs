using System;


namespace BO
{
    public class FeedbackBO
    {
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Country { get; set; }
        public bool unmericfeedback { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string FeedbackDescription { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string VisitType { get; set; }
        public string VisitNumber { get; set; }
    }
}
