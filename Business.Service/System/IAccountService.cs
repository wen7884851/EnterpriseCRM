using Core.Tool;
using Domain.Site.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.System
{
    public interface IAccountService
    {
        UserProfile GetAllUser();

        UserProfile GetUserById(int id);

        UserProfile GetUserByQuery(UserQueryWhere query);

        #region UserMessage
        OperationResult LoginCheck();

        OperationResult ChangePassWord(UserProfile user);
        #endregion

        #region PersonProfile
        OperationResult UpdateUser(UserProfile user);
        OperationResult UpdateUsers(IList<UserProfile> users);
        OperationResult DeleteUsers(IList<int> userIds);
        #endregion
    }
}
