using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class FooterQuickLinkRepository: IFooterQuickLinkRepository
    {

        private string SqlConnectionSTring;
        public FooterQuickLinkRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<GetAllFooterQuickLinkMasterResult> GetAllFacilitesMaster(long lgLangId)
        {
            using (FooterQuickLinkDataContext db = new FooterQuickLinkDataContext(SqlConnectionSTring))
            {
                return db.GetAllFooterQuickLinkMaster(lgLangId).ToList();
            }
        }

        public GetAllFooterQuickLinkMasterResult GetFacilitesMasterById(long lgId, long lgLangId)
        {
            using (FooterQuickLinkDataContext db = new FooterQuickLinkDataContext(SqlConnectionSTring))
            {
                return GetAllFacilitesMaster(lgLangId).Where(x => x.Id == lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateFooterQuickLinkMaster(GetAllFooterQuickLinkMasterResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FooterQuickLinkDataContext db = new FooterQuickLinkDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateFooterQuickLinkMaster(objData.FooterId, objData.LangId, objData.InternalOrExternal, objData.DisplaySection, objData.NameMenu, objData.InternalLink, objData.IsActive, SessionWrapper.UserDetails.UserName);
                    if (dataIsDone != null)
                    {
                        if (objData.Id == 0)
                        {
                            strError = "Record Inserted Successfully";
                            isError = false;
                        }
                        else if (objData.Id > 0)
                        {
                            strError = "Record Updated Successfully";
                            isError = false;
                        }
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

        public bool RemoveFooterQuickLinkMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FooterQuickLinkDataContext db = new FooterQuickLinkDataContext(SqlConnectionSTring))
                {
                    db.RemoveFooterQuickLinkMaster(lgId, SessionWrapper.UserDetails.UserName);
                    db.SubmitChanges();
                    strError = "Record Removed Successfully";
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
        
        public bool FooterQuickLinkMasterSwap(string cmd, decimal? col_menu_level, int col_parent_id, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (FooterQuickLinkDataContext db = new FooterQuickLinkDataContext(SqlConnectionSTring))
                {
                    db.FooterQuickLinkMasterSwap(cmd, col_menu_level, col_parent_id);
                    strError = "Record Swap Successfully";
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

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
