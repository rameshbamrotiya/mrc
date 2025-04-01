using System;
using System.Collections.Generic;
using System.Linq;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model.Hospital;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class BlogCategoryMasterRepository : IBlogCategoryMasterRepository
    {
        private string SqlConnectionSTring;
        public BlogCategoryMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        public List<BlogCategoryMasterGridModel> GetAllTblBlogCategoryMaster(int lgId)
        {
            using (BlogCategoryMasterDataContext db = new BlogCategoryMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllBlogCategoryMaster(lgId).Select(x => new BlogCategoryMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,                    
                    MetaTitle=x.MetaTitle,
                    MetaDescription=x.MetaDescription,
                    ImageName = x.ImageName,
                    ImagePath = x.ImagePath,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    BlogName = x.BlogName,
                    Blogger = x.Blogger,
                    BlogDate = Convert.ToDateTime( x.BlogDate),
                    IsVisible = x.IsVisible,
                    IsNewIcon=x.IsNewIcon,
                    TypeDetail =x.TypeDetail,

                }).ToList();
            }
        }

        public BlogCategoryMasterGridModel GetTblBlogCategoryMasterById(int lgId)
        {
            using (BlogCategoryMasterDataContext db = new BlogCategoryMasterDataContext(SqlConnectionSTring))
            {
                return db.GetBlogCategoryMasterById(lgId).Select(x => new BlogCategoryMasterGridModel
                {
                    Id = x.Id,
                    LanguageId = x.LanguageId,
                    MetaTitle = x.MetaTitle,
                    MetaDescription = x.MetaDescription,
                    ImageName = x.ImageName,
                    ImagePath = x.ImagePath,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    BlogName = x.BlogName,
                    Blogger = x.Blogger,
                    BlogDate = Convert.ToDateTime(x.BlogDate),
                    IsVisible = x.IsVisible
                }).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateTblBlogCategoryMaster(BlogCategoryMasterGridModel objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (BlogCategoryMasterDataContext db = new BlogCategoryMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateBlogCategoryMaster(objData.Id, objData.LanguageId, objData.MetaTitle,objData.MetaDescription,objData.ImageName, objData.ImagePath, objData.ShortDescription, objData.Description, objData.BlogName, objData.Blogger, objData.BlogDate, objData.IsVisible, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveTblBlogCategoryMaster(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (BlogCategoryMasterDataContext db = new BlogCategoryMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveBlogCategoryMaster((int)lgId, SessionWrapper.UserDetails.UserName);
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

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
