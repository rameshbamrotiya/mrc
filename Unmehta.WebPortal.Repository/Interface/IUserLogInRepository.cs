using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unmehta.WebPortal.Model.Common;

namespace Unmehta.WebPortal.Repository.Interface
{
    public interface IUserLogInRepository :IDisposable
    {
        bool LogInUsernamePassword(string strUsername, string strPassword, out SessionUserModel sessionModel, out string strError);
    }
}
