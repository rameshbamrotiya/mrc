using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    [Serializable()]
    public class CandidateIndexedDetailsModels
    {
        public long? Id { get; set; }
        public long? CandidateId { get; set; }
        public string NameOfTheJournal { get; set; }
        public string Topic { get; set; }
        public string Month { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public string NationalInternational { get; set; }
        public string PublicationAcceptance { get; set; }
        public string UserName { get; set; }
    }

    [Serializable()]
    public class CandidateNonIndexedDetailsModels
    {
        public long? Id { get; set; }
        public long? CandidateId { get; set; }
        public string NameOfTheJournal { get; set; }
        public string Topic { get; set; }
        public string Month { get; set; }
        public string MonthName { get; set; }
        public string Year { get; set; }
        public string NationalInternational { get; set; }
        public string PublicationAcceptance { get; set; }
        public string UserName { get; set; }
    }

    [Serializable()]
    public class CandidateDetailsForTeachingPostsModel
    {
        public long? CandidateId { get; set; }
        public string IndependentWorkWithResult { get; set; }
        public string WorkUnderSupervision { get; set; }
        public string ConferenceAttendanceAndPaper { get; set; }
    }    
}
