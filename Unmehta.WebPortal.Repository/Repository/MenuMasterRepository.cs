using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class MenuMasterRepository: IMenuMasterRepository
    {
        private string SqlConnectionSTring;
        public MenuMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<PROC_GILTender_TenderMaster_SearchResult> GetAllTenderMaster()
        {
            using (MenuMasterDataContext db = new MenuMasterDataContext(SqlConnectionSTring))
            {
                return db.PROC_GILTender_TenderMaster_Search().ToList();
            }
        }

        public List<GetBreadCumImageByPageNameResult> GetBreadCumImageByPageName(int LangId,string strPageURL)
        {
            using (MenuMasterDataContext db = new MenuMasterDataContext(SqlConnectionSTring))
            {
                return db.GetBreadCumImageByPageName(LangId,strPageURL).ToList();
            }
        }
        public List<tbl_Menu_MasterSelectAllResult> GetAllMenuList(int LangId)
        {
            using (MenuMasterDataContext db = new MenuMasterDataContext(SqlConnectionSTring))
            {
                return db.tbl_Menu_MasterSelectAll(LangId).ToList();
            }
        }
        public List<UserRightsBO> GetAllMenuListByRole(long roleId)
        {
            using (MenuMasterDataContext db = new MenuMasterDataContext(SqlConnectionSTring))
            {
                return db.tbl_Select_Resource_ByRoleIdRights((int)roleId).Select( x=> new UserRightsBO {
                    CanAdd=x.col_isAdd,
                    CanDelete=x.col_isDelete,
                    CanUpdate=x.col_isUpdate,
                    CanView=x.col_isView,
                    MenuId=x.col_menu_id,
                    MenuUrl=x.col_menu_url,
                    parentid =x.col_parent_id,
                    roleid= (int)roleId
                }).ToList();
            }
        }

        public void Dispose()
        {
        }
    }
}
