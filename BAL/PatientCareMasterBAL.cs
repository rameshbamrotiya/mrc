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
    public class PatientCareMasterBAL : PatientCareMasterDAL
    {
        public bool InsertRecord(PatientCareMasterBO objBO)
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
        public bool UpdateRecord(PatientCareMasterBO objBO)
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
        public bool DeleteRecord(PatientCareMasterBO objBO)
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
        public DataSet SelectRecord(PatientCareMasterBO objBO)
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
        public DataSet GetTabType(PatientCareMasterBO objBO)
        {
            try
            {
                return GetTabTypeByLanguageId(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertRecord(PatientCareGeneralDetailsBO objBO, DataTable dt)
        {
            try
            {
                return Insert(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(PatientCareGeneralDetailsBO objBO, DataTable dt)
        {
            try
            {
                return Update(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetPatintGeneralDetails(PatientCareGeneralDetailsBO objBO)
        {
            try
            {
                return GetPatintGeneralDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetPatintGeneralImageDetails(PatientCareGeneralDetailsBO objBO)
        {
            try
            {
                return GetPatintGeneralImageDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFormType(int Id, int LanguageId)
        {
            try
            {
                return GetFormTypeById(Id, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteGeneralDetailsRecord(PatientCareGeneralDetailsBO objBO)
        {
            try
            {
                return DeleteGeneralDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertBrochureDetailsRecord(PatientCareBrochureDetailsBO objBO)
        {
            try
            {
                return InsertBrochureDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateBrochureDetailsRecord(PatientCareBrochureDetailsBO objBO)
        {
            try
            {
                return UpdateBrochureDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteBrochureDetailsRecord(PatientCareBrochureDetailsBO objBO)
        {
            try
            {
                return DeleteBrochureDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectBrochureDetailsRecord(PatientCareBrochureDetailsBO objBO)
        {
            try
            {
                return SelectBrochureDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetPatintCareBrochureDetails(PatientCareBrochureDetailsBO objBO)
        {
            try
            {
                return GetPatintCareBrochureDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertLeftRightContainRecord(PatientCareLeftRightContainDetailsBO objBO)
        {
            try
            {
                return InsertLeftRightContainDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateLeftRightContainRecord(PatientCareLeftRightContainDetailsBO objBO)
        {
            try
            {
                return UpdateLeftRightContainDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteLeftRightContainRecord(PatientCareLeftRightContainDetailsBO objBO)
        {
            try
            {
                return DeleteLeftRightContainDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectLeftRightContainRecord(PatientCareLeftRightContainDetailsBO objBO)
        {
            try
            {
                return SelectLeftRightContainDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetPatintCareLeftRightContainRecord(PatientCareLeftRightContainDetailsBO objBO)
        {
            try
            {
                return GetPatintCareLeftRightContainDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }        
        public DataTable GetTabList(long TabId)
        {
            try
            {
                return GetTabListByTabTypeId(TabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSubTabGeneralDetailsList(PatientCareGeneralDetailsBO objBO)
        {
            try
            {
                return GetSubTabGeneralDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetBrochureDetailsList(PatientCareGeneralDetailsBO objBO)
        {
            try
            {
                return GetBrochureDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetLeftRightContainDetailsList(PatientCareLeftRightContainDetailsBO objBO)
        {
            try
            {
                return GetLeftRightContainDetailsById(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetAllFloorDetailsList()
        {
            try
            {
                return GetFloorDetailsList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdatePageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            try
            {
                return UpdateOrder(cmd, col_menu_level, col_parent_id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SequenceNo()
        {
            try
            {
                return SelectSequenceNo();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
