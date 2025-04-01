using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.CMS;

namespace Unmehta.WebPortal.Repository.Repository.CMS
{
    public class HolidayMasterRepository: IHolidayMasterRepository
    {
        private string SqlConnectionSTring;
        public HolidayMasterRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }
        public bool InsertOrUpdateHolidayMaster(HolidayMasterModel objData, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (CMSDataContext db = new CMSDataContext(SqlConnectionSTring))
                {
                    if (objData.id == 0)
                    {
                        var dataIsDone = db.InsertHolidayMaster(Convert.ToInt32(objData.id), objData.h_date, objData.h_desc, objData.user_id, objData.ip_add, objData.IsActive);
                        if (dataIsDone != null)
                        {
                            var first = dataIsDone.FirstOrDefault();
                            if (objData.id == 0)
                            {
                                if (first.IsExist > 0)
                                {
                                    strError = "Record Already Exist.";
                                    isError = false;
                                }
                                else
                                {
                                    strError = "Record Inserted Successfully";
                                    isError = false;
                                }
                            }
                            else
                            {
                                if (first.IsExist > 0)
                                {
                                    strError = "Record Already Exist.";
                                    isError = false;
                                }
                            }
                        }
                        isError = false;
                    }
                    else
                    {
                        var dataIsDone = db.UpdateHolidayMaster(Convert.ToInt32(objData.id), objData.h_date, objData.h_desc, objData.user_id, objData.ip_add, objData.IsActive,objData.IsExist);
                        if (dataIsDone != null)
                        {
                            var first = dataIsDone.FirstOrDefault();
                            if (objData.id == 0)
                            {
                                if (first.IsExist > 0)
                                {
                                    strError = "Record Already Exist.";
                                    isError = false;
                                }
                                else
                                {
                                    strError = "Record Updated Successfully";
                                    isError = false;
                                }
                            }
                            else
                            {
                                if (first.IsExist > 0)
                                {
                                    strError = "Record Already Exist.";
                                    isError = false;
                                }
                                else
                                {
                                    strError = "Record Updated Successfully";
                                    isError = false;
                                }
                            }
                        }
                        isError = false;
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

        public bool RemoveHolidayMaster(int id, out string strError)
        {
            bool isError = false;
            strError = "";
            using (CMSDataContext db = new CMSDataContext(SqlConnectionSTring))
            {
                try {
                    db.RemoveHolidayMaster(id);
                }
                catch (Exception ex)
                {
                    isError = true;
                    strError = ex.ToString();
                }
            }
            return isError;
        }

        public List<SearchHolidayMasterResult> GetAllHolidayMaster()
        {
            using (CMSDataContext db = new CMSDataContext(SqlConnectionSTring))
            {
                return db.SearchHolidayMaster().ToList();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
