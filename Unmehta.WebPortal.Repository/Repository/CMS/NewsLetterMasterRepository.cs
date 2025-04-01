using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Repository.Interface.CMS;

namespace Unmehta.WebPortal.Repository.Repository.CMS
{
    public class NewsLetterMasterRepository : INewsLetterMasterRepository
    {

        private string SqlConnectionSTring;
        public NewsLetterMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }


        public List<GetAllNewsLetterSubScriptionResult> GetAllNewsLetterSubScription()
        {
            using (NewsLetterMasterDataContext db = new NewsLetterMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllNewsLetterSubScription().ToList();
            }
        }
        
        public bool InsertOrUpdateNewsLetterMaster(GetAllNewsLetterSubScriptionResult objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (NewsLetterMasterDataContext db = new NewsLetterMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateNewsLetterMaster(objData.Id, objData.NewsLetterEmail, objData.NewsLetterSubscription);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }

                        objData.Id = (long)dataIsDone.FirstOrDefault().RecId;
                    }
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


        public bool UpdateNewsLetterMasterSubscription(long id,bool isSub, out string strError)
        {
            bool isError = false;
            strError = "";
            using (NewsLetterMasterDataContext db = new NewsLetterMasterDataContext(SqlConnectionSTring))
            {
                try
                {
                    db.UpdateNewsLetterSubScription(id, isSub);
                }
                catch (Exception ex)
                {
                    isError = true;
                    strError = ex.ToString();
                }
            }
            return isError;
        }

        public bool RemoveNewsLetterMaster(long id, out string strError)
        {
            bool isError = false;
            strError = "";
            using (NewsLetterMasterDataContext db = new NewsLetterMasterDataContext(SqlConnectionSTring))
            {
                try
                {
                    db.RemoveNewsLetterMaster(id);
                }
                catch (Exception ex)
                {
                    isError = true;
                    strError = ex.ToString();
                }
            }
            return isError;
        }


        public void Dispose()
        {

        }
    }
}
