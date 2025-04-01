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
    public class PackageMasterBAL:PackageMasterDAL
    {
        public bool InsertRecord(PackageMasterBO objBO, DataTable dt)
        {
            try
            {
                return Insert(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRecord(PackageMasterBO objBO, DataTable dt)
        {
            try
            {
                return Update(objBO, dt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteRecord(PackageMasterBO objBO)
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
        public DataSet SelectRecord(PackageMasterBO objBO)
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
        public DataSet SelectRecordPackageDetails(PackageSubMasterBO objBO)
        {
            try
            {
                return SelectSubPackageDetails(objBO);
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
        public DataSet FillPackageTypeById(int langId,int TypeId)
        {
            try
            {
                return FillPackageTypeId(langId, TypeId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FillPackageType(int langId)
        {
            try
            {
                return GetPackageType(langId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertSubPackageRecord(PackageSubMasterBO objBO)
        {
            try
            {
                return InsertSubPackage(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateSubPackageRecord(PackageSubMasterBO objBO)
        {
            try
            {
                return UpdateSubPackage(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSubPackageGrid(int PackageDetailId)
        {
            try
            {
                return GetSubPackageGridData(PackageDetailId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectSubPackageRecord(int Id)
        {
            try
            {
                return SelectSubPackage(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteSubPackageRecord(PackageSubMasterBO objBO)
        {
            try
            {
                return DeleteSubPackage(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateSubPageOrder(string cmd, string col_menu_level, string col_parent_id)
        {
            try
            {
                return UpdateSubOrder(cmd, col_menu_level, col_parent_id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SubPageSequenceNo()
        {
            try
            {
                return SelectSubPageSequenceNo();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
