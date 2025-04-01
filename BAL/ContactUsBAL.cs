using BO;
using DAL;
using System;
using System.Data;

namespace BAL
{
    public class ContactUsBAL : ContactUsDAL
    {
        public DataSet SelectRecordCkEditor(int LanguageId,string MenuURL)
        {
            try
            {
                return SelectCkeditor(LanguageId, MenuURL);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
