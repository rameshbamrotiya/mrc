using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Rights;
using Unmehta.WebPortal.Model.Model;

namespace Unmehta.WebPortal.Repository.Repository.Rights
{
    public class ChatBotRepository : IDisposable
    {

        private string SqlConnectionSTring;
        public ChatBotRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }
        public void Dispose()
        {
        }


        public List<GetAllChatBotDetailsResult> GetAllChatBotDetails()
        {
            using (ChatBoxMasterDataContext db = new ChatBoxMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllChatBotDetails().ToList();
            }
        }

        public bool InsertChatBotDetails(ChatBoxModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (ChatBoxMasterDataContext db = new ChatBoxMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertChatBotDetails(objData.Name, objData.IsSkipEmail, objData.EmailId, objData.YouLocation, objData.Phone, objData.IsSkipPastMedicalHistory, objData.PastMedicalHistory, objData.IsSkipPresentMedicalHistory, objData.PresentMedicalHistory, objData.WriteQuery, objData.IPAddress);
                    
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }



        public bool RemoveChatBotDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ChatBoxMasterDataContext db = new ChatBoxMasterDataContext(SqlConnectionSTring))
                {
                    var data = db.RemoveChatBotDetails(lgId);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

    }
}
