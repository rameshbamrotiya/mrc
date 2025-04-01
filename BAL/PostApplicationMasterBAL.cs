using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DAL;
using System.Data;

namespace BAL
{
    public class PostApplicationMasterBAL:PostApplicationMasterDAL
    {
        public bool InsertRecord(PostApplicationMasterBO objBO)
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

        public DataSet SelectRecord(int Id, string spName)
        {
            try
            {
                return Select(Id, spName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertQualificationRecord(QualificationMasterBo objBO)
        {
            try
            {
                return InsertQualification(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertJobApplicationRecord(JobApplicationMasterBo objBO)
        {
            try
            {
                return InsertJobApplication(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteRecord(int Id, string spName)
        {
            try
            {
                return Delete(Id, spName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetQualificationDetails(int IsEducation)
        {
            try
            {
                return GetAllQualificationDetails(IsEducation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetPostJobApplication(int JobId)
        {
            try
            {
                return GetAllPostJobApplication(JobId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetPostMasterDetails()
        {
            try
            {
                return GetAllPostMasterDetails();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetAllQualification(string spName)
        {
            try
            {
                return GetQualification(spName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
