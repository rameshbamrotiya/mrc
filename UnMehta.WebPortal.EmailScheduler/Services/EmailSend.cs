using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Repository.Interface.Hospital;
using Unmehta.WebPortal.Repository.Repository.Hospital;
using UnMehta.WebPortal.EmailScheduler.Comman;
using Unmehta.WebPortal.Data;
using System.Net.Mail;
using System.IO;
using Unmehta.WebPortal.Data.Hospital;

namespace UnMehta.WebPortal.EmailScheduler.Services
{
    public class EmailSend
    {
        public void StartEmail()
        {

            string strPath = Functions.strWebHostedPath;

            using (IScheduleNewsLetterRepository objBlogCategoryMasterRepository = new ScheduleNewsLetterRepository(Functions.strSqlConnectionString))
            {
                int currentDay = DateTime.Now.Day;

                var allSchedule = objBlogCategoryMasterRepository.GetAllScheduleNewsLetterMaster().Where(x => x.StartDate == currentDay).OrderBy(x => x.Id).ToList();


                foreach (var schedule in allSchedule)
                {
                    string strError = "";

                    var findAllLog = objBlogCategoryMasterRepository.GetAllScheduleNewsLetterMasterLog()
                        .Where(x => x.ScheduleId == schedule.Id && x.TriggerDateTime.Value.Date == DateTime.Now.Date).Count();

                    if (findAllLog <= 0)
                    {

                        #region Schedule Start Log
                        GetAllScheduleNewsLetterMasterLogResult objLog = new GetAllScheduleNewsLetterMasterLogResult();
                        objLog.MailSubject = schedule.MailSubject;
                        objLog.MailDescription = schedule.MailDescription;
                        objLog.ScheduleId = schedule.Id;
                        objLog.StartDate = schedule.StartDate;
                        objLog.TriggerDateTime = DateTime.Now;
                        objLog.DocId = schedule.DocId;
                        objLog.Id = 0;
                        objLog.LogDescription = "Schedule Trigger Start";
                        if (objBlogCategoryMasterRepository.InsertScheduleNewsLetterMasterLog(objLog, out strError))
                        {
                            ErrorLogger.ERROR("Schedule Trigger Log Have Error=>  " + strError, "");
                        }
                        #endregion
                        
                        #region Schedule Logic
                        ErrorLogger.ERROR("Schedule Trigger Subject=>  " + schedule.MailSubject, "Start");

                        var getAllEmail = objBlogCategoryMasterRepository.GetAllSendEMailNewsLetterSubscriber();

                        if (!string.IsNullOrWhiteSpace(strPath) && schedule.DocId > 0)
                        {

                            List<Attachment> lstAttachment = new List<Attachment>();

                            string filePath = strPath+schedule.DocPath.Replace("~", "").Replace(@"//", @"/");

                            if (File.Exists(filePath))
                            {
                                lstAttachment.Add(new System.Net.Mail.Attachment(filePath));
                            }

                            //var allEmail = objBlogCategoryMasterRepository.GetAllNewsLetterSubscriber().Where(x=> Functions.ValidateEmailId(x.EmailId)).OrderBy(x => x.Id).ToList();

                            foreach (var email in getAllEmail)
                            {
                                try
                                {
                                    bool isSend = false;

                                    #region Email Send Start Log
                                    if (objLog.Id != null)
                                    {
                                        if (objLog.Id > 0)
                                        {
                                            GetAllScheduleNewsLetterMasterEmailLogResult objLogEmail = new GetAllScheduleNewsLetterMasterEmailLogResult();
                                            objLogEmail.MailSubject = schedule.MailSubject;

                                            objLogEmail.EmailId = email.EmailId;
                                            //objLogEmail.FullName = email.FullName;
                                            //objLogEmail.MobileNo = email.MobileNo;
                                            objLogEmail.FullName = "";
                                            objLogEmail.MobileNo = "";
                                            objLogEmail.flag = isSend;
                                            //objLogEmail.Location = email.Location;
                                            objLogEmail.Location = "";

                                            objLogEmail.MailDescription = schedule.MailDescription;
                                            objLogEmail.ScheduleId = schedule.Id;
                                            objLogEmail.StartDate = schedule.StartDate;
                                            objLogEmail.TriggerDateTime = DateTime.Now;
                                            objLogEmail.DocId = schedule.DocId;
                                            objLogEmail.LogId = objLog.Id;
                                            objLogEmail.Id = 0;
                                            objLogEmail.LogDescription = "Email Send to " + email.EmailId + " Start";
                                            if (objBlogCategoryMasterRepository.InsertScheduleNewsLetterMasterEmailLog(objLogEmail, out strError))
                                            {
                                                ErrorLogger.ERROR("Schedule Trigger Log Have Error=>  " + strError, "");
                                            }
                                        }
                                    }
                                    #endregion


                                    ErrorLogger.ERROR("Schedule Trigger Subject=>  " + schedule.MailSubject + " Email is => " + email.EmailId, "Start");
                                    string strMessage = "";
                                    if (!Functions.SendEmail(email.EmailId, schedule.MailSubject, schedule.MailDescription, out strMessage, true, lstAttachment))
                                    {
                                        ErrorLogger.ERROR("Email Not Send to " + email.EmailId, strMessage);
                                    }
                                    else
                                    {
                                        ErrorLogger.ERROR("Email Send to " + email.EmailId, strMessage);
                                        isSend = true;
                                    }
                                    ErrorLogger.ERROR("Schedule Trigger Subject=>  " + schedule.MailSubject + " Email is => " + email.EmailId, "End");

                                    #region Email Send End Log
                                    if (objLog.Id != null)
                                    {
                                        if (objLog.Id > 0)
                                        {
                                            GetAllScheduleNewsLetterMasterEmailLogResult objLogEmail = new GetAllScheduleNewsLetterMasterEmailLogResult();
                                            objLogEmail.MailSubject = schedule.MailSubject;

                                            objLogEmail.EmailId = email.EmailId;
                                            //objLogEmail.FullName = email.FullName;
                                            //objLogEmail.MobileNo = email.MobileNo;
                                            objLogEmail.flag = isSend;
                                            //objLogEmail.Location = email.Location;

                                            objLogEmail.FullName = "";
                                            objLogEmail.MobileNo = "";
                                            objLogEmail.flag = isSend;
                                            objLogEmail.Location = "";

                                            objLogEmail.MailDescription = schedule.MailDescription;
                                            objLogEmail.ScheduleId = schedule.Id;
                                            objLogEmail.StartDate = schedule.StartDate;
                                            objLogEmail.TriggerDateTime = DateTime.Now;
                                            objLogEmail.DocId = schedule.DocId;
                                            objLogEmail.LogId = objLog.Id;
                                            objLogEmail.Id = 0;
                                            objLogEmail.LogDescription = "Email Send to " + email.EmailId + " End";
                                            if (objBlogCategoryMasterRepository.InsertScheduleNewsLetterMasterEmailLog(objLogEmail, out strError))
                                            {
                                                ErrorLogger.ERROR("Schedule Trigger Log Have Error=>  " + strError, "");
                                            }
                                        }
                                    }
                                    #endregion
                                }catch(Exception ex)
                                {
                                    ErrorLogger.ERROR("Schedule Trigger Subject=>  " + schedule.MailSubject + " Email is => " + email.EmailId +" Error is => "+ex.Message+ " \r\n  Description=> "+ex.ToString() , "End");
                                }
                            }
                        }
                        else
                        {
                            //var allEmail = objBlogCategoryMasterRepository.GetAllNewsLetterSubscriber().OrderBy(x => x.Id).ToList();

                            foreach (var email in getAllEmail)
                            {

                                bool isSend = false;

                                #region Email Send Start Log
                                if (objLog.Id != null)
                                {
                                    if (objLog.Id > 0)
                                    {
                                        GetAllScheduleNewsLetterMasterEmailLogResult objLogEmail = new GetAllScheduleNewsLetterMasterEmailLogResult();
                                        objLogEmail.MailSubject = schedule.MailSubject;

                                        objLogEmail.EmailId = email.EmailId;
                                        //objLogEmail.FullName = email.FullName;
                                        //objLogEmail.MobileNo = email.MobileNo;
                                        objLogEmail.flag = isSend;
                                        //objLogEmail.Location = email.Location;

                                        objLogEmail.FullName = "";
                                        objLogEmail.MobileNo = "";
                                        objLogEmail.flag = isSend;
                                        objLogEmail.Location = "";

                                        objLogEmail.MailDescription = schedule.MailDescription;
                                        objLogEmail.ScheduleId = schedule.Id;
                                        objLogEmail.StartDate = schedule.StartDate;
                                        objLogEmail.TriggerDateTime = DateTime.Now;
                                        objLogEmail.DocId = schedule.DocId;
                                        objLogEmail.LogId = objLog.Id;
                                        objLogEmail.Id = 0;
                                        objLogEmail.LogDescription = "Email Send to " + email.EmailId + " Start";
                                        if (objBlogCategoryMasterRepository.InsertScheduleNewsLetterMasterEmailLog(objLogEmail, out strError))
                                        {
                                            ErrorLogger.ERROR("Schedule Trigger Log Have Error=>  " + strError, "");
                                        }
                                    }
                                }
                                #endregion


                                ErrorLogger.ERROR("Schedule Trigger Subject=>  " + schedule.MailSubject + " Email is => " + email.EmailId, "Start");
                                string strMessage = "";
                                if (!Functions.SendEmail(email.EmailId, schedule.MailSubject, schedule.MailDescription, out strMessage, true))
                                {
                                    ErrorLogger.ERROR("Email Not Send to " + email.EmailId, strMessage);
                                }
                                else
                                {
                                    ErrorLogger.ERROR("Email Send to " + email.EmailId, strMessage);
                                    isSend = true;
                                }
                                ErrorLogger.ERROR("Schedule Trigger Subject=>  " + schedule.MailSubject + " Email is => " + email.EmailId, "End");

                                #region Email Send End Log
                                if (objLog.Id != null)
                                {
                                    if (objLog.Id > 0)
                                    {
                                        GetAllScheduleNewsLetterMasterEmailLogResult objLogEmail = new GetAllScheduleNewsLetterMasterEmailLogResult();
                                        objLogEmail.MailSubject = schedule.MailSubject;

                                        objLogEmail.EmailId = email.EmailId;
                                        //objLogEmail.FullName = email.FullName;
                                        //objLogEmail.MobileNo = email.MobileNo;
                                        objLogEmail.flag = isSend;
                                        //objLogEmail.Location = email.Location;

                                        objLogEmail.FullName = "";
                                        objLogEmail.MobileNo = "";
                                        objLogEmail.flag = isSend;
                                        objLogEmail.Location = "";

                                        objLogEmail.MailDescription = schedule.MailDescription;
                                        objLogEmail.ScheduleId = schedule.Id;
                                        objLogEmail.StartDate = schedule.StartDate;
                                        objLogEmail.TriggerDateTime = DateTime.Now;
                                        objLogEmail.DocId = schedule.DocId;
                                        objLogEmail.LogId = objLog.Id;
                                        objLogEmail.Id = 0;
                                        objLogEmail.LogDescription = "Email Send to " + email.EmailId + " End";
                                        if (objBlogCategoryMasterRepository.InsertScheduleNewsLetterMasterEmailLog(objLogEmail, out strError))
                                        {
                                            ErrorLogger.ERROR("Schedule Trigger Log Have Error=>  " + strError, "");
                                        }
                                    }
                                }
                                #endregion

                            }
                        }
                        #endregion

                        #region Schedule End Log
                        GetAllScheduleNewsLetterMasterLogResult objLogs = new GetAllScheduleNewsLetterMasterLogResult();
                        objLogs.MailSubject = schedule.MailSubject;
                        objLogs.MailDescription = schedule.MailDescription;
                        objLogs.ScheduleId = schedule.Id;
                        objLogs.StartDate = schedule.StartDate;
                        objLogs.TriggerDateTime = DateTime.Now;
                        objLogs.DocId = schedule.DocId;
                        objLogs.Id = 0;
                        objLogs.LogDescription = "Schedule Trigger End";
                        if (objBlogCategoryMasterRepository.InsertScheduleNewsLetterMasterLog(objLogs, out strError))
                        {
                            ErrorLogger.ERROR("Schedule Trigger Log Have Error=>  " + strError, "");
                        }
                        #endregion

                        ErrorLogger.ERROR("Schedule Trigger Subject=>  " + schedule.MailSubject, "End");
                    }
                    else
                    {

                        ErrorLogger.ERROR("Schedule Already Trigger Subject=>  " + schedule.MailSubject, "");
                    }
                }
            }
        }
    }
}
