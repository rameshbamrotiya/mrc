using System;
using BO;
using DAL;
using System.Data;

namespace BAL
{
    public class ExtraInfoforAdmissionBAL:ExtraInfoforAdmissionDAL
    {
        public bool InsertRecord(ExtraInfoforAdmissionBO objBO)
        {
            try
            {
                return Insert(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecordReferralDetails(ExtraInfoforAdmissionBO objBO)
        {
            try
            {
                return InsertReferralDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(ExtraInfoforAdmissionBO objBO)
        {
            try
            {
                return Delete(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(ExtraInfoforAdmissionBO objBO)
        {
            try
            {
                return Select(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordReferral(ExtraInfoforAdmissionBO objBO)
        {
            try
            {
                return SelectReferralDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAdvertisementSourceWise()
        {
            try
            {
                return SelectAdvertisementSource();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet Candidate_Select(int Id, long CourseId)
        {
            try
            {
                return CandidateSelect(Id, CourseId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetStudentDetail(int Id, long CourseId)
        {
            try
            {
                return GetStudentDetails(Id, CourseId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertFinalSubmit(int Id, long CourseId, int IsFinalSubmit)
        {
            try
            {
                return InsertFinal(Id, CourseId, IsFinalSubmit);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
