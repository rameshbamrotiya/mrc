using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class PatientFeedbacksRepository : IPatientFeedbacksRepository
    {
        private string SqlConnectionSTring;
        public PatientFeedbacksRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<PatientFeedbackGridModel> GetAllTblPatientFeedback(long lgId)
        {
            using (PatientFeedbackDataContext db = new PatientFeedbackDataContext(SqlConnectionSTring))
            {
                return db.GetAllPatientFeedback(lgId).Select(x => new PatientFeedbackGridModel
                {
                    Id = x.Id,
					Name = x.Name,
                    Gender = x.Gender,
                    MobileNo = x.MobileNo,
                    EmailId = x.EmailId,
                    FeedBackDetails = x.FeedBackDetails,
                    IsVisible = x.IsVisible
                }).ToList();
            }
        }

        public PatientFeedbackGridModel GetTblPatientFeedbackById(int lgId)
        {
            using (PatientFeedbackDataContext db = new PatientFeedbackDataContext(SqlConnectionSTring))
            {
                return db.GetPatientFeedbackById(lgId).Select(x => new PatientFeedbackGridModel
                {
                    Id = x.Id,
					Name = x.Name,
                    Gender = x.Gender,
                    MobileNo = x.MobileNo,
                    EmailId = x.EmailId,
                    FeedBackDetails = x.FeedBackDetails,
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblPatientFeedback(PatientFeedbackGridModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (PatientFeedbackDataContext db = new PatientFeedbackDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdatePatientFeedback(objData.Id, objData.LanguageId, objData.Name, objData.Gender, objData.MobileNo, objData.EmailId, objData.FeedBackDetails, objData.IsVisible);
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

        public bool RemoveTblPatientFeedback(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (PatientFeedbackDataContext db = new PatientFeedbackDataContext(SqlConnectionSTring))
                {
                    db.RemovePatientFeedback(lgId);
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

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
