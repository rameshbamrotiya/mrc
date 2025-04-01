using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class EventMasterBO
    {
        public int? LanguageId { get; set; }
        public int? EventId { get; set; }
        public int? id { get; set; }
        public string EventName { get; set; }
        public string EventTypeId { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }
        public string Seat { get; set; }
        public string Organizer { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Websitelink { get; set; }
        public string OrganizedBy { get; set; }
        public string EventDetails { get; set; }
        public string MainImg { get; set; }
        public string InnerImg { get; set; }
        public string EventGalalry { get; set; }
        public string StartTimeHH { get; set; }
        public string StartTimeMM { get; set; }
        public string StartTimeTT { get; set; }
        public bool? IsOnlineRegistration { get; set; }
        public string IsActive { get; set; }
        public int? IsExist { get; set; }
        public int? user_id { get; set; }
        public string ip_add { get; set; }
    }
}
