﻿using System.Linq;
using Domain.Site.Models;
using Domain.DB.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Core.Service.Authen
{
    public interface IUserService
    {
        #region 属性
        IQueryable<User> Users { get; }
        #endregion

        #region 公共方法
        UserAccountViewModel GetAccountByLoginName(string LoginName);
        IQueryable<UserViewModel> GetAllUser();
        UserViewModel GetUserModelById(int userId);
        UserViewModel GetUserModelByToken(string token);
        List<UserViewModel> GetUserListByQuery(UserSearchViewModel query);
        UserProfileViewModel GetUserProfileById(int userId);
        ActionResultViewModel UpdateUserProfile(UserProfileViewModel model);
        ActionResultViewModel CheckLogin(UserAccountViewModel user);
        ActionResultViewModel ChangeUserPassWord(UserAccountViewModel user);
        ActionResultViewModel UploadUserPhoto(UserPhotoViewModel model);
        #endregion
    }
}
