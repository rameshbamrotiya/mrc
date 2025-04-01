using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class ICUOnWheelRepository : IDisposable
    {
        private string SqlConnectionSTring;

        public ICUOnWheelRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }
        
        #region Main Details
        public List<GetAllICUOnWheelMasterResult> GetAllICUOnWheelMaster()
        {
            using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllICUOnWheelMaster().ToList();
            }
        }

        public GetAllICUOnWheelMasterResult GetAllICUOnWheelMasterById(long lgId)
        {
            using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
            {
                return GetAllICUOnWheelMaster().FirstOrDefault(x => x.Id == lgId);
            }
        }

        public GetAllICUOnWheelMainDescResult GetAllICUOnWheelMainDesc()
        {
            using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllICUOnWheelMainDesc().FirstOrDefault();
            }
        }

        public bool InsertOrUpdateICUOnWheelMainDesc(GetAllICUOnWheelMainDescResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateICUOnWheelMainDesc(objData.Id, objData.ICUOnWheelDesc, SessionWrapper.UserDetails.UserName);
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
                        objData.Id = (long)dataIsDone.FirstOrDefault().RecId;
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

        public bool InsertOrUpdateICUOnWheelMaster(GetAllICUOnWheelMasterResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateICUOnWheelMaster(objData.Id, objData.ICUName, objData.ICUDetails, SessionWrapper.UserDetails.UserName);
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
                        objData.Id = (long)dataIsDone.FirstOrDefault().RecId;
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

        public bool RemoveICUOnWheelMaster(int lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveICUOnWheelMaster(lgId, SessionWrapper.UserDetails.UserName);
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
        #endregion

        #region Sub Details

        public List<GetAllICUOnWheelSubDetailsResult> GetAllICUOnWheelSubDetails()
        {
            using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllICUOnWheelSubDetails().ToList();
            }
        }

        public GetAllICUOnWheelSubDetailsResult GetAllICUOnWheelSubDetailsById(long lgId)
        {
            using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
            {
                return GetAllICUOnWheelSubDetails().FirstOrDefault(x => x.Id == lgId);
            }
        }

        public bool InsertOrUpdateICUOnWheelSubDetails(GetAllICUOnWheelSubDetailsResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateICUOnWheelSubDetails(objData.Id, objData.MainId, objData.SubTitle, objData.SubDescription, objData.ImageName, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveICUOnWheelSubDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveICUOnWheelSubDetails(lgId, SessionWrapper.UserDetails.UserName);
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

        #endregion

        #region Image Details

        public List<GetAllICUOnWheelImageDetailsResult> GetAllICUOnWheelImageDetails()
        {
            using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllICUOnWheelImageDetails().ToList();
            }
        }

        public GetAllICUOnWheelImageDetailsResult GetAllICUOnWheelImageDetailsById(long lgId)
        {
            using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
            {
                return GetAllICUOnWheelImageDetails().FirstOrDefault(x => x.Id == lgId);
            }
        }

        public bool InsertOrUpdateICUOnWheelImageDetails(GetAllICUOnWheelImageDetailsResult objData, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateICUOnWheelImageDetails(objData.Id, objData.MainId, objData.ImageName, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveICUOnWheelImageDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (ICUOnWheelMasterDataContext db = new ICUOnWheelMasterDataContext(SqlConnectionSTring))
                {
                    db.RemoveICUOnWheelImageDetails(lgId, SessionWrapper.UserDetails.UserName);
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



        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
