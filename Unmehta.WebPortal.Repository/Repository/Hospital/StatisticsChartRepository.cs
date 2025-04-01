using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Hospital;
using Unmehta.WebPortal.Model.Model;
using Unmehta.WebPortal.Repository.Interface.Hospital;

namespace Unmehta.WebPortal.Repository.Repository.Hospital
{
    public class StatisticsChartRepository : IStatisticsChartRepository
    {
        private string SqlConnectionSTring;
        public StatisticsChartRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }

        #region Statistics Chart Main

        public List<GetAllStatisticsChartMasterResult> GetAllStatisticsChart()
        {
            using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllStatisticsChartMaster().ToList();
            }
        }

        public GetAllStatisticsChartMasterResult GetStatisticsChartById(long lgId)
        {
            using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
            {
                return GetAllStatisticsChart().Where(x=> x.Id==lgId).FirstOrDefault();
            }
        }

        public bool StatisticsChartMasterSwap(string cmd, decimal? col_menu_level, int col_parent_id, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    db.StatisticsChartMasterSwap(cmd, col_menu_level, col_parent_id);
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

        public bool InsertOrUpdateStatisticsChart(GetAllStatisticsChartMasterResult objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.InsertOrUpdateStatisticsChartMaster(objData.Id, objData.ChartName, objData.ChartType, objData.XValueName, objData.YValueName, objData.XValueFormate, objData.IsActive, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveStatisticsChart(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    db.RemvoeStatisticsChartMaster(lgId, SessionWrapper.UserDetails.UserName);
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

        #region Statistics Chart Data 

        public List<GetAllStatisticsChartMasterDetailsResult> GetAllStatisticsChartDetails()
        {
            using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllStatisticsChartMasterDetails().ToList();
            }
        }

        public List<GetAllStatisticsChartMasterDetailsByChartIdResult> GetAllStatisticsChartDetailsByChartId(long lgChartId)
        {
            using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllStatisticsChartMasterDetailsByChartId(lgChartId).ToList();
            }
        }

        public GetAllStatisticsChartMasterDetailsResult GetStatisticsChartDetailsById(long lgId)
        {
            using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
            {
                return GetAllStatisticsChartDetails().Where(x => x.Id == lgId).FirstOrDefault();
            }
        }

        public bool InsertOrUpdateStatisticsChartDetails(StatisticsChartDetailsModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    //var dataIsDone = db.InsertOrUpdateStatisticsChartMasterDetails(objData.Id, objData.ChartId,objData.SequanceNo,objData.ColumnName, objData.ColumnValue, SessionWrapper.UserDetails.UserName);
                    //if (dataIsDone != null)
                    //{
                    //    if (objData.Id == 0)
                    //    {
                    //        strError = "Record Inserted Successfully";
                    //        isError = false;
                    //    }
                    //    else if (objData.Id > 0)
                    //    {
                    //        strError = "Record Updated Successfully";
                    //        isError = false;
                    //    }
                    //    objData.Id = (long)dataIsDone.FirstOrDefault().RecId;
                        

                    //}
                    //isError = false;

                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllStatisticsChartDetails();
                        if (objData.SwapToSequanceNo != null && objData.SwapFromSequanceNo != null)
                        {

                            if (objData.SwapToSequanceNo > 0 && objData.SwapFromSequanceNo > 0)
                            {
                                long minSqNo = (long)dataList.FirstOrDefault().SequanceNo, maxSequNo = (long)dataList.OrderByDescending(x => x.SequanceNo).FirstOrDefault().SequanceNo;

                                if (!(minSqNo < objData.SwapToSequanceNo || maxSequNo >= objData.SwapToSequanceNo))
                                {
                                    strError = "Swap Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                    isError = false;
                                }
                                else
                                {
                                    if (objData.SwapType == "Swap")
                                    {
                                        if (!(minSqNo < objData.SwapFromSequanceNo || maxSequNo >= objData.SwapFromSequanceNo))
                                        {
                                            strError = "Current Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                            isError = false;
                                        }
                                        else
                                        {
                                            var SwapDetail = dataList.Where(x => x.SequanceNo == objData.SwapToSequanceNo).FirstOrDefault();
                                            if (SwapDetail != null)
                                            {
                                                var swapValueUpdate = db.InsertOrUpdateStatisticsChartMasterDetails(SwapDetail.Id, SwapDetail.ChartId, SwapDetail.ColumnId, objData.SwapFromSequanceNo, SwapDetail.ColumnName, SwapDetail.ColumnValue, SwapDetail.AliasName, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateStatisticsChartMasterDetails(objData.Id, objData.ChartId, objData.ColumnId, objData.SequanceNo, objData.ColumnName, objData.ColumnValue, objData.AliasName, SessionWrapper.UserDetails.UserName);
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
                                            else
                                            {
                                                strError = "Swap Sequence No Details Dose Not Exist " + "Swap Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                                isError = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objData.SequanceNo = objData.SwapToSequanceNo;
                                        if (!isError)
                                        {
                                            var dataIsDone = db.InsertOrUpdateStatisticsChartMasterDetails(objData.Id, objData.ChartId, objData.ColumnId, objData.SequanceNo, objData.ColumnName, objData.ColumnValue, objData.AliasName, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {
                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<GetAllStatisticsChartMasterDetailsResult> lstLop = dataList.Where(x => x.SequanceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdateStatisticsChartMasterDetails(data.Id, objData.ChartId, data.ColumnId, currentSwqNo, data.ColumnName, data.ColumnValue, data.AliasName, SessionWrapper.UserDetails.UserName);
                                                    }
                                                }
                                                else
                                                {
                                                    List<GetAllStatisticsChartMasterDetailsResult> lstLop = dataList.Where(x => x.SequanceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        var swapValueUpdate = db.InsertOrUpdateStatisticsChartMasterDetails(data.Id, objData.ChartId, data.ColumnId, currentSwqNo, data.ColumnName, data.ColumnValue, data.AliasName, SessionWrapper.UserDetails.UserName);

                                                    }
                                                }
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
                                }
                            }
                            else
                            {
                                if (!isError)
                                {
                                    var dataIsDone = db.InsertOrUpdateStatisticsChartMasterDetails(objData.Id, objData.ChartId, objData.ColumnId, objData.SequanceNo, objData.ColumnName, objData.ColumnValue, objData.AliasName, SessionWrapper.UserDetails.UserName);
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
                        }
                        else
                        {
                            if (!isError)
                            {
                                var dataIsDone = db.InsertOrUpdateStatisticsChartMasterDetails(objData.Id, objData.ChartId, objData.ColumnId, objData.SequanceNo, objData.ColumnName, objData.ColumnValue, objData.AliasName, SessionWrapper.UserDetails.UserName);
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

                    }
                    else
                    {
                        if (!isError)
                        {
                            var dataIsDone = db.InsertOrUpdateStatisticsChartMasterDetails(objData.Id, objData.ChartId, objData.ColumnId, objData.SequanceNo, objData.ColumnName, objData.ColumnValue, objData.AliasName, SessionWrapper.UserDetails.UserName);
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
                    #endregion
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveStatisticsChartDetails(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    db.RemvoeStatisticsChartMasterDetails(lgId, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveStatisticsChartDetailByChartId(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    db.RemvoeStatisticsChartMasterDetailsByChartId(lgId);
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

        #region Statistics Chart Column 
        
        public List<GetAllStatisticsChartMasterColumnListByChartIdResult> GetAllStatisticsChartColumnListByChartId(long lgChartId)
        {
            using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
            {
                return db.GetAllStatisticsChartMasterColumnListByChartId(lgChartId).ToList();
            }
        }

        public GetAllStatisticsChartMasterColumnListByChartIdResult GetStatisticsChartColumnListById(long lgChartId,long lgId)
        {
            using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
            {
                return GetAllStatisticsChartColumnListByChartId(lgChartId).Where(x => x.Id == lgId).FirstOrDefault();
            }
        }


        public bool InsertOrUpdateStatisticsChartColumnList(StatisticsChartColumnListModel objData, out string strError)
        {
            //_Education_Qualification obj_Education_Qualifications = new _Education_Qualification();
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    #region Swap Logic
                    if (objData.Id != 0)
                    {
                        var dataList = GetAllStatisticsChartColumnListByChartId((long)objData.ChartId);
                        if (objData.SwapToSequanceNo != null && objData.SwapFromSequanceNo != null)
                        {

                            if (objData.SwapToSequanceNo > 0 && objData.SwapFromSequanceNo > 0)
                            {
                                long minSqNo = (long)dataList.FirstOrDefault().SequanceNo, maxSequNo = (long)dataList.OrderByDescending(x => x.SequanceNo).FirstOrDefault().SequanceNo;

                                if (!(minSqNo < objData.SwapToSequanceNo || maxSequNo >= objData.SwapToSequanceNo))
                                {
                                    strError = "Swap Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                    isError = false;
                                }
                                else
                                {
                                    if (objData.SwapType == "Swap")
                                    {
                                        if (!(minSqNo < objData.SwapFromSequanceNo || maxSequNo >= objData.SwapFromSequanceNo))
                                        {
                                            strError = "Current Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                            isError = false;
                                        }
                                        else
                                        {
                                            var SwapDetail = dataList.Where(x => x.SequanceNo == objData.SwapToSequanceNo).FirstOrDefault();
                                            if (SwapDetail != null)
                                            {
                                                var swapValueUpdate = db.InsertOrUpdateStatisticsChartMasterColumnList(SwapDetail.Id, SwapDetail.ChartId, objData.SwapFromSequanceNo, SwapDetail.TypeColumn, SwapDetail.ColName, SessionWrapper.UserDetails.UserName);
                                                objData.SequanceNo = objData.SwapToSequanceNo;
                                                if (!isError)
                                                {
                                                    var dataIsDone = db.InsertOrUpdateStatisticsChartMasterColumnList(objData.Id, objData.ChartId, objData.SequanceNo, objData.TypeColumn, objData.ColName, SessionWrapper.UserDetails.UserName);
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
                                            else
                                            {
                                                strError = "Swap Sequence No Details Dose Not Exist " + "Swap Sequence No Must be Between " + minSqNo + " to " + maxSequNo;
                                                isError = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objData.SequanceNo = objData.SwapToSequanceNo;
                                        if (!isError)
                                        {
                                            var dataIsDone = db.InsertOrUpdateStatisticsChartMasterColumnList(objData.Id, objData.ChartId, objData.SequanceNo, objData.TypeColumn, objData.ColName, SessionWrapper.UserDetails.UserName);
                                            if (dataIsDone != null)
                                            {
                                                if (objData.SwapToSequanceNo < objData.SwapFromSequanceNo)
                                                {
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    List<GetAllStatisticsChartMasterColumnListByChartIdResult> lstLop = dataList.Where(x => x.SequanceNo >= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo++;
                                                        var swapValueUpdate = db.InsertOrUpdateStatisticsChartMasterColumnList(data.Id, objData.ChartId, currentSwqNo, data.TypeColumn, data.ColName, SessionWrapper.UserDetails.UserName);
                                                    }
                                                }
                                                else
                                                {
                                                    List<GetAllStatisticsChartMasterColumnListByChartIdResult> lstLop = dataList.Where(x => x.SequanceNo <= objData.SwapToSequanceNo && x.Id != objData.Id).ToList();
                                                    long currentSwqNo = objData.SwapToSequanceNo;
                                                    foreach (var data in lstLop)
                                                    {
                                                        currentSwqNo--;
                                                        var swapValueUpdate = db.InsertOrUpdateStatisticsChartMasterColumnList(data.Id, objData.ChartId, currentSwqNo, data.TypeColumn, data.ColName, SessionWrapper.UserDetails.UserName);

                                                    }
                                                }
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
                                }
                            }
                            else
                            {
                                if (!isError)
                                {
                                    var dataIsDone = db.InsertOrUpdateStatisticsChartMasterColumnList(objData.Id, objData.ChartId,  objData.SequanceNo, objData.TypeColumn, objData.ColName, SessionWrapper.UserDetails.UserName);
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
                        }
                        else
                        {
                            if (!isError)
                            {
                                var dataIsDone = db.InsertOrUpdateStatisticsChartMasterColumnList(objData.Id, objData.ChartId, objData.SequanceNo, objData.TypeColumn, objData.ColName, SessionWrapper.UserDetails.UserName);
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

                    }
                    else
                    {
                        if (!isError)
                        {
                            var dataIsDone = db.InsertOrUpdateStatisticsChartMasterColumnList(objData.Id, objData.ChartId, objData.SequanceNo, objData.TypeColumn, objData.ColName, SessionWrapper.UserDetails.UserName);
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
                    #endregion
                }
            }
            catch (Exception ex)
            {
                isError = true;
                strError = ex.ToString();
            }
            return isError;
        }

        public bool RemoveStatisticsChartColumnList(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    db.RemvoeStatisticsChartMasterColumnList(lgId, SessionWrapper.UserDetails.UserName);
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

        public bool RemoveStatisticsChartColumnListByChartId(long lgId, out string strError)
        {
            bool isError = false;
            strError = "";
            try
            {
                using (StatisticsChartMasterDataContext db = new StatisticsChartMasterDataContext(SqlConnectionSTring))
                {
                    db.RemvoeStatisticsChartMasterColumnListByChartId(lgId);
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
