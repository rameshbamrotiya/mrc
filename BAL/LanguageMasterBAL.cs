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
    public class LanguageMasterBAL
    {
        LanguageMasterDal objLanguageMasterDal = new LanguageMasterDal();
        public IEnumerable<LanguageMasterBO> GetAllLanguage()
        {
            try
            {
                return objLanguageMasterDal.GetAllLanguage();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet FillLanguage()
        {
            try
            {
                return objLanguageMasterDal.GetLanguages();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
