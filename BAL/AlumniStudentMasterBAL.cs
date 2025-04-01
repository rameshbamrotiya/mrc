using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class AlumniStudentMasterBAL : AlumniStudentMasterDAL
    {
        public bool InsertRecord(AlumniStudentMasterBO objBO)
        {
            try
            {
                return InsertAlumniStudentDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(AlumniStudentMasterBO objBO)
        {
            try
            {
                return UpdateAlumniStudentDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAlumniStudentDetails(AlumniStudentMasterBO objBO)
        {
            try
            {
                return GetAllAlumniStudentDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAlumniStudentDetailsFront(AlumniStudentMasterBO objBO)
        {
            try
            {
                return GetAllAlumniStudentDetailsFront(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAlumniStudentByID(AlumniStudentMasterBO objbo)
        {
            try
            {
                return GetAlumniStudentDetailsById(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(AlumniStudentMasterBO objBO)
        {
            try
            {
                return DeleteAlumniStudentDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
