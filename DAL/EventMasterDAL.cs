using BO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DAL
{
    public class EventMasterDAL:DBConnection
    {
        protected bool RemoveEventSocialMedia(long EventId)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "RemoveEventMasterSocialmediaLinks";
                command.Parameters.AddWithValue("@EventId", EventId);

                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected bool InsertSocialMedia(long EventId, string SocialMediaName, string SocialMediaLink, string user_id, string ip_add)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertEventMasterSocialmediaLinks";
                command.Parameters.AddWithValue("@EventId", EventId);
                command.Parameters.AddWithValue("@SocialMediaName", SocialMediaName);
                command.Parameters.AddWithValue("@SocialMediaLink", SocialMediaLink);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@ip_add", ip_add);
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Insert(EventMasterBO objbo, DataTable dtPatronlist, DataTable dtSocialmediaLinks)
        {
            try
            {

                DataSet ds = new DataSet();

                objbo.MainImg = string.IsNullOrWhiteSpace(objbo.MainImg) ? "" : objbo.MainImg;
                objbo.InnerImg = string.IsNullOrWhiteSpace(objbo.InnerImg) ? "" : objbo.InnerImg;
                objbo.EventGalalry = string.IsNullOrWhiteSpace(objbo.EventGalalry) ? "" : objbo.EventGalalry;

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_EventMaster_Insert";
                command.Parameters.AddWithValue("@EventId", 0).Direction = ParameterDirection.Output;
                command.Parameters.AddWithValue("@EventName", objbo.EventName);
                command.Parameters.AddWithValue("@EventTypeId", objbo.EventTypeId);
                command.Parameters.AddWithValue("@EventStartDate", objbo.EventStartDate);
                command.Parameters.AddWithValue("@EventEndDate", objbo.EventEndDate);
                command.Parameters.AddWithValue("@Venue", objbo.Venue);
                command.Parameters.AddWithValue("@Location", objbo.Location);
                command.Parameters.AddWithValue("@StartTimeHH", objbo.StartTimeHH);
                command.Parameters.AddWithValue("@StartTimeMM", objbo.StartTimeMM);
                command.Parameters.AddWithValue("@StartTimeTT", objbo.StartTimeTT);
                command.Parameters.AddWithValue("@Seat", objbo.Seat);
                command.Parameters.AddWithValue("@Organizer", objbo.Organizer);
                command.Parameters.AddWithValue("@Phone", objbo.Phone);
                command.Parameters.AddWithValue("@Email", objbo.Email);
                command.Parameters.AddWithValue("@Websitelink", objbo.Websitelink);
                command.Parameters.AddWithValue("@OrganizedBy", objbo.OrganizedBy);
                command.Parameters.AddWithValue("@EventDetails", objbo.EventDetails);
                command.Parameters.AddWithValue("@MainImg", objbo.MainImg);
                command.Parameters.AddWithValue("@InnerImg", objbo.InnerImg);
                command.Parameters.AddWithValue("@EventGalalry", objbo.EventGalalry);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@IsOnlineRegistration", objbo.IsOnlineRegistration);
                command.Parameters.AddWithValue("@Is_Active", objbo.IsActive);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtPatronlist", dtPatronlist);
                //command.Parameters.AddWithValue("@dtSocialmediaLinks", dtSocialmediaLinks);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                //command.Parameters.AddWithValue("@EventId", 0).Direction = ParameterDirection.Output;


                ds = ExecuteQuery(command);
                //ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());
                int EventId = 0;
                if (dtSocialmediaLinks.Rows.Count > 0 && int.TryParse(ds.Tables[0].Rows[0]["EventId"].ToString(),out EventId))
                {
                    objbo.EventId = EventId ;
                    RemoveEventSocialMedia((long)objbo.EventId);
                    foreach (DataRow row in dtSocialmediaLinks.Rows)
                    {
                        InsertSocialMedia((long)objbo.EventId, row["SocialMediaName"].ToString(), row["SocialMediaLink"].ToString(), objbo.user_id.ToString(), objbo.ip_add);
                    }
                }

                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool Update(EventMasterBO objbo, DataTable dtPatronlist, DataTable dtSocialmediaLinks)
        {
            try
            {
                objbo.MainImg = string.IsNullOrWhiteSpace(objbo.MainImg) ? "" : objbo.MainImg;
                objbo.EventGalalry = string.IsNullOrWhiteSpace(objbo.EventGalalry) ? "" : objbo.EventGalalry;
                objbo.InnerImg = string.IsNullOrWhiteSpace(objbo.InnerImg) ? "" : objbo.InnerImg;

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_EventMaster_Update";
                command.Parameters.AddWithValue("@EventId", objbo.EventId);
                command.Parameters.AddWithValue("@EventName", objbo.EventName);
                command.Parameters.AddWithValue("@EventTypeId", objbo.EventTypeId);
                command.Parameters.AddWithValue("@EventStartDate", objbo.EventStartDate);
                command.Parameters.AddWithValue("@EventEndDate", objbo.EventEndDate);
                command.Parameters.AddWithValue("@Venue", objbo.Venue);
                command.Parameters.AddWithValue("@Location", objbo.Location);
                command.Parameters.AddWithValue("@StartTimeHH", objbo.StartTimeHH);
                command.Parameters.AddWithValue("@StartTimeMM", objbo.StartTimeMM);
                command.Parameters.AddWithValue("@StartTimeTT", objbo.StartTimeTT);
                command.Parameters.AddWithValue("@Seat", objbo.Seat);
                command.Parameters.AddWithValue("@Organizer", objbo.Organizer);
                command.Parameters.AddWithValue("@Phone", objbo.Phone);
                command.Parameters.AddWithValue("@Email", objbo.Email);
                command.Parameters.AddWithValue("@Websitelink", objbo.Websitelink);
                command.Parameters.AddWithValue("@OrganizedBy", objbo.OrganizedBy);
                command.Parameters.AddWithValue("@EventDetails", objbo.EventDetails);
                command.Parameters.AddWithValue("@MainImg", objbo.MainImg);
                command.Parameters.AddWithValue("@InnerImg", objbo.InnerImg);
                command.Parameters.AddWithValue("@EventGalalry", objbo.EventGalalry);
                command.Parameters.AddWithValue("@LanguageId", objbo.LanguageId);
                command.Parameters.AddWithValue("@IsOnlineRegistration", objbo.IsOnlineRegistration);
                command.Parameters.AddWithValue("@Is_Active", objbo.IsActive);
                command.Parameters.AddWithValue("@user_id", objbo.user_id);
                command.Parameters.AddWithValue("@ip_add", objbo.ip_add);
                command.Parameters.AddWithValue("@dtPatronlist", dtPatronlist);
                //command.Parameters.AddWithValue("@dtSocialmediaLinks", dtSocialmediaLinks);
                command.Parameters.AddWithValue("@IsExist", 0).Direction = ParameterDirection.Output;
                ExecuteNonQuery(command);
                objbo.IsExist = int.Parse(command.Parameters["@IsExist"].Value.ToString());


                if (dtSocialmediaLinks.Rows.Count > 0)
                {
                    RemoveEventSocialMedia((long)objbo.EventId);
                    foreach (DataRow row in dtSocialmediaLinks.Rows)
                    {
                        InsertSocialMedia((long)objbo.EventId, row["SocialMediaName"].ToString(), row["SocialMediaLink"].ToString(), objbo.user_id.ToString(), objbo.ip_add);
                    }
                }

                if (objbo.IsExist > 0) return false; else return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool Delete(EventMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_EventMaster_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet Select(EventMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_EventMaster_Select");
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectPatronlist(EventMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_EventMasterPatronlist_Select");
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectSocialmediaLinks(EventMasterBO objbo)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_EventMasterSocialmediaLinks_Select");
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeletePatronlist(EventMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_EventMasterPatronlist_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected bool DeleteSocialmediaLinks(EventMasterBO objbo)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                CreateParameters(objbo, ref command, "PROC_EventMasterSocialmediaLinks_Delete");
                ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet GetEventType(int LangId)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();

                command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetEventTypeLanguageWise";
                command.Parameters.AddWithValue("@Language_id", LangId);
                ds = ExecuteQuery(command);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected DataSet SelectEventFrontByLanguage(string EventName = "", int EventType = 0, int eventid = 0, int LanguageId = 0)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PROC_Event_Selectallbylanguage";
                command.Parameters.AddWithValue("@LanguageId", LanguageId);
                command.Parameters.AddWithValue("@eventid", eventid);
                command.Parameters.AddWithValue("@EventName", EventName);
                command.Parameters.AddWithValue("@EventType", EventType);
                ExecuteNonQuery(command);
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
