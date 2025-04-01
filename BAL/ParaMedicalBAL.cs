using BO;
using DAL;
using System;
using System.Data;


namespace BAL
{
   public class ParaMedicalBAL:ParaMedicalDAL
    {
        public bool InsertRecord(ParamedicalBO objBO)
        {
            try
            {
                return InsertParaMedicalCourse(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateRecord(ParamedicalBO objBO)
        {
            try
            {
                return Update(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllCourse(ParamedicalBO objbo)
        {
            try
            {
                return SelectAllCourse(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet getCourseByIDAndLanguage(ParamedicalBO objbo)
        {
            try
            {
                return SelectCourseByIDAndLanguage(objbo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
