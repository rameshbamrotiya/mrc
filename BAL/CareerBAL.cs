using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class CareerBAL:CareerDAL
    {
        public DataSet SelectRecord()
        {
            try
            {
                return Select();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllCareerRecord()
        {
            try
            {
                return GetAllCareerRecords();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllCareerRecordNotification()
        {
            try
            {
                return GetAllCareerRecordsNotification();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordJob(int rid = 0)
        {
            try
            {
                return SelectJoblist(rid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordJobSearch(string designation = "", string PostType = "", string RecruitmentType = "")
        {
            try
            {
                return SelectJoblistsearch(designation, PostType, RecruitmentType);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordJobwalin(int rid = 0)
        {
            try
            {
                return SelectJoblistwalkin(rid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectCareerDetails(int Rid = 0)
        {
            try
            {
                return SelectDetails(Rid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecord(UploadCVCareer objBO)
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
        public DataTable GetAllCareerCVRecord(string startdate, string enddate)
        {
            try
            {
                return GetAllCareerCV(startdate,enddate);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllCareerCVAllRecord()
        {
            try
            {
                return GetAllCareerCVAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
