using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BO;
using BAL;
using System.Data;

namespace BAL
{
    public class TemplateMasterBAL : TemplateMasterDAL
    {
        public DataSet Get_All_TemplateMaster()
        {
            try
            {
                return GetAllTemplateMaster();
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public bool Insert_Update_TemplateMaster(TemplateMasterBO objBO)
        {
            try
            {
                return InsertOrUpdateTemplateMaster(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool Delete_Template(int TempID)
        {
            try
            {
                return DeleteTemplateMaster(TempID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet TemplateMaster_SelectByTemplateID(int TempID)
        {try
            {
                return GetTemplateMasterById(TempID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<GetTemplateMasterByIdAndLangIdResult> GetTemplateMasterByIdLangId(int TempID,int LangId)
        {
            try
            {
                return GetTemplateMasterByIdAndLangId(TempID, LangId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetPageDataFromPageId(string pageName, int languageId)
        {try
            {
                return GetPageDataFromPageIdDetail(pageName, languageId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<GetAllPageListWithMaskingUrlResult> GetAllPageListWithUrl()
        {
            try
            {
                return GetAllPageListWithMaskingUrl();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
