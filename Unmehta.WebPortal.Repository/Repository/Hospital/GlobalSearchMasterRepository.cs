using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class GlobalSearchMasterRepository : IGlobalSearchMasterRepository
    {

        private string SqlConnectionSTring;
        public GlobalSearchMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }


        public List<GetAllDetailMetaDescriptionResult> GetAllongAboutUsMaster(long lgLangId,string strSearch)
        {
            using (GlobalSearchMasterDataContext db = new GlobalSearchMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllDetailMetaDescription(lgLangId, strSearch).ToList();
            }
        }

        public void Dispose()
        {

        }
    }
}
