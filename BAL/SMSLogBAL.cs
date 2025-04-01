using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
   public class SMSLogBAL : SMSLogDAL
    {
        public bool InsertRecord(SMSlogBO objBO)
        {
            try
            {
                return InsertSMSLog(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
