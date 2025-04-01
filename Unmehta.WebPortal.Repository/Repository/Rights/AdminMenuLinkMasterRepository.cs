using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Rights;
using Unmehta.WebPortal.Model.Model.Rights;
using Unmehta.WebPortal.Repository.Interface.Rights;

namespace Unmehta.WebPortal.Repository.Repository.Rights
{
    public class AdminMenuLinkMasterRepository : IAdminMenuLinkMasterRepository
    {

        private string SqlConnectionSTring;
        public AdminMenuLinkMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public void Dispose()
        {
        }

        public List<GetAllAdminMenuMasterResult> GetAllAdminMenuMaster()
        {
            using (AdminMenuLinkDataContext db = new AdminMenuLinkDataContext(SqlConnectionSTring))
            {
                return db.GetAllAdminMenuMaster().ToList();
            }
        }

        public bool InsertOrUpdateAdminMenuLinkMaster(AdminMenuLinkModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AdminMenuLinkDataContext db = new AdminMenuLinkDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateAdminMenuMaster(objData.Id, objData.MenuName,objData.MenuUrl,objData.ParentId, objData.IsActive, objData.IPAddress, "");
                    if (objData.Id == 0)
                    {

                        strError = "Record Saved Successfully";
                    }
                    else
                    {

                        strError = "Record updated Successfully";
                    }
                    isError = false;
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveAdminMenuLinkMaster(long lgId, string userName, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (AdminMenuLinkDataContext db = new AdminMenuLinkDataContext(SqlConnectionSTring))
                {
                    var data = db.RemoveAdminMenuMaster(lgId, userName);
                    if(data!=null)
                    {
                        if(data.FirstOrDefault().RecId>0)
                        {
                            strError = "Record Removed Successfully";
                            isError = false;

                        }
                        else
                        {

                            strError = "Record is Fixed So unable to remove";
                            isError = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }
    }
}
