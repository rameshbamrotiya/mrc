using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Data.Common;
using Unmehta.WebPortal.Model.Common;
using Unmehta.WebPortal.Repository.Interface;

namespace Unmehta.WebPortal.Repository.Repository
{
    public class UserLogInRepository : IUserLogInRepository
    {
        private string SqlConnectionSTring;
        public UserLogInRepository(string strConnection)
        {
            SqlConnectionSTring = strConnection;
        }


        public bool LogInUsernamePassword(string strUsername,string strPassword,out SessionUserModel sessionModel, out string strError)
        {
            //Tbl_Education_Qualification objTbl_Education_Qualifications = new Tbl_Education_Qualification();
            sessionModel = new SessionUserModel();
            bool isError = false;
            strError = "";
            try
            {
                using (UserLoginDataContext db = new UserLoginDataContext(SqlConnectionSTring))
                {
                    var dataIsDone = db.GetLoginUsersMaster(strUsername, strPassword).Select(x=> new SessionUserModel {
                        Id=x.Id,
                        DepartmentId=x.Id,
                        Designation=x.Designation,
                        Email=x.Email,
                        FirstName=x.FirstName,
                        LastName=x.LastName,
                        IsActive=x.IsActive,
                        PhoneNo=x.PhoneNo,
                        RoleId=x.RoleId,
                        UserName=x.UserName,
                        UserPassword=x.UserPassword
                    }).FirstOrDefault();

                    if (dataIsDone != null)
                    {
                        sessionModel = dataIsDone;
                        isError = false;
                    }
                    else
                    {
                        strError = "User Detail Not Found.";
                        isError = true;
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


        public void Dispose()
        {
        }
    }
}
