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
    public class PhotoGalleryMasterBAL : PhotoGalleryMasterDAL
    {
        public bool InsertRecord(PhotogalleryMasterBO objBO,DataTable dt)
        {
            try
            {
                return Insert(objBO,dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(PhotogalleryMasterBO objBO,DataTable dt)
        {
            try
            {
                return Update(objBO,dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(PhotogalleryMasterBO objBO)
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
        public bool DeleteRecordImg(PhotogalleryMasterBO objBO)
        {
            try
            {
                return DeleteImg(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecord(PhotogalleryMasterBO objBO)
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
        public DataSet SelectRecordImg(PhotogalleryMasterBO objBO)
        {
            try
            {
                return Selectimg(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRecordalbum(PhotogalleryMasterBO objBO)
        {
            try
            {
                return Selectalbum(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectalbumImg(int albumid,int LanguageId)
        {
            try
            {
                return Selectalbumimg(albumid,LanguageId);
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
        public DataSet SelectalTagListGalleyDetails(int LanguageId)
        {
            try
            {
                return SelectalTagListGalley(LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectalVideoTagListGalleyDetails(int LanguageId)
        {
            try
            {
                return SelectalVideoTagListGalley(LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectalAlbumGalleyDetails(int albumid, int LanguageId)
        {
            try
            {
                return SelectalAlbumGalley(albumid, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectalVideoGalleyDetails(int videoId, int LanguageId)
        {
            try
            {
                return SelectalVideoGalley(videoId, LanguageId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
