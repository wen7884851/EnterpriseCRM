using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Repository.Authen;
using Domain.DB.Models;
using Domain.Site.Models;
using Domain.Site.Models.Authen.User;
using Framework.Common.FileHelper;
using Framework.Common.SecurityHelper;
using Framework.Common.ToolsHelper;
using Framework.Common.ToolsHelper.Net;
using Framework.Tool.Operator;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Core.Service.Authen.Impl
{
    /// <summary>
    /// 服务层实现 —— UserService
    /// </summary>
    [Export(typeof(IUserService))]
    public class UserService : CoreServiceBase, IUserService
    {
        #region 属性

        [Import]
        public IUserRepository UserRepository { get; set; }
        [Import]
        public IUserProfileRepository _userProfileRepository { get; set; }

        public IQueryable<User> Users
        {
            get { return UserRepository.NoCahecEntities; }
        }

        private IQueryable<User> UsersByCahec
        {
            get { return UserRepository.Entities; }
        }
        #endregion

        #region 公共方法
        public UserViewModel GetUserModelByToken(string token)
        {
            var user = Users.FirstOrDefault(t => t.Token == token);
            if (user != null)
            {
                return Mapper.Map<UserViewModel>(user);
            }
            return null;
        }
        public UserAccountViewModel GetAccountByLoginName(string LoginName)
        {
            var user = Users.FirstOrDefault(t => ((t.LoginName == LoginName) || t.Email == LoginName )|| t.Phone == LoginName);
            if(user!=null)
            {
                return new UserAccountViewModel()
                {
                    userId=user.Id,
                    LoginName=user.LoginName,
                    LoginPwd=user.LoginPwd,
                    PwdErrorCount=user.PwdErrorCount,
                    Enabled=user.Enabled,
                    LastLoginTime=user.LastLoginTime
                };
            }
            return null;
        }
        public List<UserViewModel> GetUserListByQuery(UserSearchViewModel query)
        {
            var expr = BuildSearchUser(query);
            var userList = Users.Where(expr).OrderByDescending(t => t.CreateTime).Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
            .Select(t=>new UserViewModel() {
                Id=t.Id,
                LoginName=t.LoginName,
                FullName=t.Profile.FirstOrDefault().FullName,
                Email=t.Email,
                Phone=t.Phone,
                RoleName="测试",
                LastLoginTime=t.LastLoginTime
            }).ToList();
            return userList;
        }

        private Expression<Func<User, bool>> BuildSearchUser(UserSearchViewModel query)
        {
            var bulider = new DynamicLambda<User>();
            Expression<Func<User, bool>> expr = t => t.IsDeleted == false;
            if (!string.IsNullOrEmpty(query.LoginName))
            {
                Expression<Func<User, bool>> tmp = t => t.LoginName.Contains(query.LoginName.Trim());
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (query.FullName != null)
            {
                Expression<Func<User, bool>> tmp = t => t.Profile.Select(x=>x.FullName).Contains(query.FullName);
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            return expr;
        }
        public ActionResultViewModel UpdateUserProfile(UserProfileViewModel model)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = false,
            };
            if (model.UserId == 0)
            {
                result.Result = "用户ID有误";
                return result;
            }
            if (model.IDCardNo == "")
            {
                result.Result = "用户IDCard不能为空";
                return result;
            }
            var user = UsersByCahec.FirstOrDefault(t => (t.Id == model.UserId) && t.IsDeleted == false);
            if (user == null)
            {
                result.Result = "用户数据不存在";
                return result;
            }

            UpdateProfile(model, user);
            result.IsSuccess = true;
            return result;
        }

        private void UpdateProfile(UserProfileViewModel model, User user)
        {
            user.Phone = model.Phone;
            user.Email = model.Email;
            var profile = user.Profile.FirstOrDefault();
            profile.FullName = model.FullName;
            profile.IDCardNo = model.IDCardNo;
            profile.EmergencyContact = model.EmergencyContact;
            profile.EmergencyPhone = model.EmergencyPhone;
            profile.Education = model.Education;
            profile.Aptitude = model.Aptitude;
            profile.Address = model.Address;
            profile.Motto = model.Motto;
            profile.Birthday = profile.IDCardNo.Substring(6, 4) + "-" + profile.IDCardNo.Substring(10, 2) + "-" + profile.IDCardNo.Substring(12, 2);
            string sexCode = profile.IDCardNo.Substring(14, 3);
            if (int.Parse(sexCode) % 2 == 0)
            {
                profile.Sex = 0;
            }
            else
            {
                profile.Sex = 1;
            }
            UserRepository.Update(user);
            _userProfileRepository.Update(profile);
        }

        public UserProfileViewModel GetUserProfileById(int userId)
        {
            var user = Users.FirstOrDefault(t => t.Id == userId);
            var profile = Mapper.Map<UserProfileViewModel>(user.Profile.FirstOrDefault());
            profile.PhotoPath = user.PhotoPath;
            profile.Phone = user.Phone;
            profile.Email = user.Email;
            return profile;
        }
        public UserViewModel GetUserModelById(int userId)
        {
            var user = Users.FirstOrDefault(t => t.Id == userId);
            if (user != null)
            {
               return  Mapper.Map<UserViewModel>(user);
            }
            return null;
        }

        public IQueryable<UserViewModel> GetAllUser()
        {
            return Users.Where(t=>t.IsDeleted==false).Select(t => new UserViewModel()
            {
                Id=t.Id,
                LoginName = t.LoginName
            });
        }
        public ActionResultViewModel CheckLogin(UserAccountViewModel user)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = false,
            };

            if ((string.IsNullOrEmpty(user.LoginName)) && string.IsNullOrEmpty(user.LoginPwd))
            {
                result.Result = "用户名密码为空，请重新输入！";
                return result;
            }
            var userAccount =GetAccountByLoginName(user.LoginName);
            if (userAccount == null)
            {
                result.Result = "用户名不存在，请确定后重新输入！";
                return result;
            }
            if ((userAccount.PwdErrorCount > 5) && userAccount.LastLoginTime < System.DateTime.Now.AddMinutes(5))
            {
                result.Result = "用户名输入密码错误次数超过5次，请5分钟后再登录！";
                return result;
            }
            if ( user.LoginPwd != MD5Provider.GetMD5String(userAccount.LoginPwd).ToLower())
            {
                result.Result = "密码错误，请确定后重新输入！";
                UpdateUserLoginError(userAccount.userId);
                return result;
            }
            if(!userAccount.Enabled)
            {
                result.Result = "该用户已禁用！";
                return result;
            }
            result=Login(userAccount.userId);
            return result;
        }

        public ActionResultViewModel ChangeUserPassWord(UserAccountViewModel user)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = false,
            };
            var userDto = Users.FirstOrDefault(t => t.Id == user.userId);
            if (user.LoginPwd != MD5Provider.GetMD5String(userDto.LoginPwd).ToLower())
            {
                result.Result = "用户原密码输入错误,请重新输入！";
                return result;
            }
            userDto.LoginPwd = user.NewLoginPwd;
            userDto.isFirstLogin = false;
            UserRepository.Update(userDto);
            result.IsSuccess = true;
            return result;
        }

        public ActionResultViewModel UploadUserPhoto(UserPhotoViewModel model)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = false,
            };
            if (model.UserId == 0)
            {
                result.Result = "用户ID有误！";
                return result;
            }
            var userDTO = Users.FirstOrDefault(t => t.Id == model.UserId);
            DeletePhoto(model, userDTO.PhotoPath);
            userDTO.PhotoPath = model.FileName;
            UserRepository.Update(userDTO);
            result.IsSuccess = true;
            result.Result = model.FileName;
            return result;
        }
        #endregion

        #region 私有方法
        private void DeletePhoto(UserPhotoViewModel model,string fileName)
        {
            if (fileName != null&& fileName != "")
            {
                FileOperate.DelFile(model.FilePath + fileName);
            }
        }
        private void UpdateUserLoginError(int userId)
        {
            var user = Users.FirstOrDefault(t => t.Id == userId);
            if(user.PwdErrorCount==0)
            {
                user.PwdErrorCount++;
                user.LastLoginTime = System.DateTime.Now;
            }
            else if (user.PwdErrorCount<=5)
            {
                user.PwdErrorCount++;
            }
            else
            {
                if(user.LastLoginTime < System.DateTime.Now.AddMinutes(-5))
                {
                    user.LastLoginTime = System.DateTime.Now;
                    user.PwdErrorCount = 1;
                }
            }
            UserRepository.Update(user);
        }

        private ActionResultViewModel Login(int userId,bool isRemeberUserName=false)
        {
            var user = Users.FirstOrDefault(t => t.Id == userId);
            user.IsRemeberUserName = isRemeberUserName;
            user.Token = Guid.NewGuid().ToString("N");
            user.LastLoginTime= DateTime.Now;
            var result = new OperatorModel()
            {
                UserId= user.Id,
                LoginName=user.LoginName,
                Email = user.Email,
                Phone = user.Phone,
                PwdErrorCount= user.PwdErrorCount,
                LoginCount= user.LoginCount,
                RegisterTime= user.RegisterTime,
                Token = user.Token,
                IsFirstLogin=user.isFirstLogin,
                PhotoPath=user.PhotoPath
            };
            result.LoginIPAddress = Net.Ip;
            result.LoginIPAddressName = Net.Host;
            OperatorProvider.Provider.AddCurrent(result);
            UserRepository.Update(user);
            return new ActionResultViewModel()
            {
                IsSuccess = true,
                Token = user.Token,
                Result = "/Common/Dashboard/Index"
            };
        }
        #endregion
    }
}
