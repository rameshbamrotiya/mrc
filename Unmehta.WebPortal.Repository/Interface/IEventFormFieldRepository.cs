using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IEventFormFieldRepository : IDisposable
    {
        List<GetAllOnlineEventRegistrtionResult> GetAllOnlineEventRegistrtion(long lgEventId);

        List<GetAllEventFormFieldMasterByEventIdResult> GetAlllongAboutUsMaster(long lgEventId);

        bool InsertEventFormFieldMaster(GetAllEventFormFieldMasterByEventIdResult objData);

        bool RemoveEventFormFieldMasterByEventId(int lgId);

        bool RemoveRecordOnlineEventRegistrtion(int lgId);
    }
}
