using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Model.Model.Hospital;

namespace Unmehta.WebPortal.Repository.Interface.Hospital
{
    public interface IBlogCategoryMasterRepository : IDisposable
    {
        List<BlogCategoryMasterGridModel> GetAllTblBlogCategoryMaster(int lgId);

        BlogCategoryMasterGridModel GetTblBlogCategoryMasterById(int lgId);

        bool InsertOrUpdateTblBlogCategoryMaster(BlogCategoryMasterGridModel objData, out string strError);

        bool RemoveTblBlogCategoryMaster(long lgId, out string strError);
    }
}
