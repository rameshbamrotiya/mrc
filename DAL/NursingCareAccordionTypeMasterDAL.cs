using BO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class NursingCareAccordionTypeMasterDAL : DBConnection
    {
        protected bool Insert(NursingCareAccordionTypeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareAccordionTypeMaster_Insert";
                command.Parameters.AddWithValue("@Accordion_Name", objbo.Accordion_Name);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(NursingCareAccordionTypeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareAccordionTypeMaster_Update";
                command.Parameters.AddWithValue("@Accordion_id", objbo.Accordion_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Accordion_Name", objbo.Accordion_Name);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Delete(NursingCareAccordionTypeMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareAccordionTypeMaster_Delete";
                command.Parameters.AddWithValue("@Accordion_id", objbo.Accordion_id);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(NursingCareAccordionTypeMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareAccordionTypeMaster_Select";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@Accordion_id", objbo.Accordion_id);
                ExecuteNonQuery(command);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool InsertAccrodianSub(NursingCareAccordionTypeSubMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareAccordionTypeSubMaster_Insert";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@AccordionSubTitle", objbo.AccordionSubTitle);
                command.Parameters.AddWithValue("@AccordionType_id", objbo.AccordionType_id);
                command.Parameters.AddWithValue("@AccordionSubDescription", objbo.AccordionSubDescription);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                ExecuteNonQuery(command);
                return true;


            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool UpdateAccrodianSub(NursingCareAccordionTypeSubMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareAccordionTypeSubMaster_Update";
                command.Parameters.AddWithValue("@AccordionSub_id", objbo.AccordionSub_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@AccordionSubTitle", objbo.AccordionSubTitle);
                command.Parameters.AddWithValue("@AccordionSubDescription", objbo.AccordionSubDescription);
                command.Parameters.AddWithValue("@AccordionType_id", objbo.AccordionType_id);
                command.Parameters.AddWithValue("@Is_active", objbo.Is_active);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);

                ExecuteNonQuery(command);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteAccrodianSub(NursingCareAccordionTypeSubMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_NursingCareAccordionTypeSubMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAccrodianSub(NursingCareAccordionTypeSubMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareAccordionTypeSubMaster_Select";
                command.Parameters.AddWithValue("@AccordionSub_id", objbo.AccordionSub_id);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAccrodianType(NursingCareAccordionTypeSubMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetNursingCareAccordionTypeMaster";
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectAccrodianSubFront(int LanguageId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_NursingCareAccordionTypeSubMaster_Front";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
