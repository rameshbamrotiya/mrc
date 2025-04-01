using System;
using BO;
using DAL;
using System.Data;

namespace BAL
{
   public class GeneralinstructionBAL: GeneralinstructionDAL
    {
        public bool InsertRecord(GeneralinstructionBO objBO)
        {
            try
            {
                return InsertOrUpdateGeneralInstructionDetails(objBO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveRecord(int Id, string Username)
        {
            try
            {
                return RemoveGeneralInstruction(Id,Username);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetAllInstruction()
        {
            try
            {
                return SelectAllGeneralInstruction();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
