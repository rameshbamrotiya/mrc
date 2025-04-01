using BO;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BAL
{
    public class OPDTimingsBAL:OPDTimingsDAL
    {
        public bool InsertRecord(OPDTimingsBO objBO, DataTable dt,DataTable dtunit)
        {
            try
            {
                return Insert(objBO, dt, dtunit);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(OPDTimingsBO objBO, DataTable dt, DataTable dtunit)
        {
            try
            {
                return Update(objBO, dt, dtunit);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectDoctorListByLanguage(OPDTimingsBO objBO)
        {
            try
            {
                return GetDoctorListLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectUnitListByLanguage(OPDTimingsBO objBO)
        {
            try
            {
                return GetUnitListLanguageWise(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(OPDTimingsBO objBO)
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
        public DataSet SelectRecord(OPDTimingsBO objBO)
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
        public DataSet SelectRecordDoctor(OPDTimingsBO objBO)
        {
            try
            {
                return SelectDoctor(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordUnit(OPDTimingsBO objBO)
        {
            try
            {
                return SelectUnit(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecordDoctor(OPDTimingsBO objBO)
        {
            try
            {
                return DeleteDoctor(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecordUnit(OPDTimingsBO objBO)
        {
            try
            {
                return DeleteUnit(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFacultylistforUnitConfig(facultyNameForUnitConfig objBO)
        {
            try
            {
                return selectGetFacultylistforUnitConfig(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
