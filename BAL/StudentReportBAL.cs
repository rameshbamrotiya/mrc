using System;
using BO;
using DAL;
using System.Data;

namespace BAL
{
    public class StudentReportBAL : StudentReportDAL
    {
        public DataSet GetStudentMasterData()
        {
            try
            {
                return SelectStudentMasterData();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectApplicationStatus()
        {
            try
            {
                return GetApplicationStatus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectStatusWiseRecord(string ApplicationStatus, string PaymentStatus, string Course, string startdate, string enddate, string trastartdate, string traenddate)
        {
            try
            {
                return GetApplicationStatusWise(ApplicationStatus, PaymentStatus, Course, startdate, enddate, trastartdate, traenddate);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
