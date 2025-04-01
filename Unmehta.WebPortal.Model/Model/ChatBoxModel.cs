using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unmehta.WebPortal.Model.Model
{
    [Serializable()]
    public class ChatBoxResponseModel
    {
        public string hfLastFieldName { get; set; }
        public string strChatBox { get; set; }

    }


    public enum ChatBoxEnum
    {
        Welcome,
        Name,
        EmailId,
        Location,
        Phone,
        PastMedicalHistory,
        PresentMedicalHistory,
        WriteQuery,
        End
    }

    [Serializable()]
    public class ChatBoxModel
    {
        public string Name { get; set; }
        public bool IsSkipEmail { get; set; }
        public string EmailId { get; set; }
        public string YouLocation { get; set; }
        public string Phone { get; set; }
        public bool IsSkipPastMedicalHistory { get; set; }
        public string PastMedicalHistory { get; set; }
        public bool IsSkipPresentMedicalHistory { get; set; }
        public string PresentMedicalHistory { get; set; }
        public string WriteQuery { get; set; }
        public string IPAddress { get; set; }
    }
}
