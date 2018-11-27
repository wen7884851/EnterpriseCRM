﻿using Framework.Tool.Operator;
using Domain.Site.Models.Common;
using System;
using System.Web.Mvc;

namespace Web.Site.WebUI.Common
{
    //[AdminPermission(PermissionCustomMode.Enforce)]
    public class AdminController : Controller
    {
		public AdminController()
		{
			//TODO: Test
            //var userRole = new List<UserRole> { new UserRole { Id = 1, UserId = 1, RoleId = 1 } };
            //var user = new User { Id = 1, LoginName = "admin", LoginPwd = "8wdJLK8mokI=", UserRole = userRole };
            //SessionHelper.SetSession("CurrentUser", user);
		}

        //protected User GetCurrentUser()
        //{
        //	var user = SessionHelper.GetSession("CurrentUser") as User;
        //	return user;
        //}

        protected OperatorModel GetCurrentUser()
        {
            return OperatorProvider.Provider.GetCurrent();
        }

        protected void CreateBaseData<T>(T model) where T : EntityCommon
		{
            //var user = SessionHelper.GetSession("CurrentUser") as User;
            var user = OperatorProvider.Provider.GetCurrent();
            if (user != null)
			{
				model.CreateId = user.UserId;
				model.CreateBy = user.LoginName;
				model.CreateTime = DateTime.Now;
				model.ModifyId = user.UserId;
				model.ModifyBy = user.LoginName;
				model.ModifyTime = DateTime.Now;
			}
		}

		protected void UpdateBaseData<T>(T model) where T : EntityCommon
		{
            //var user = SessionHelper.GetSession("CurrentUser") as User;
            var user = OperatorProvider.Provider.GetCurrent();
            if (user != null)
			{
				model.ModifyId = user.UserId;
				model.ModifyBy = user.LoginName;
				model.ModifyTime = DateTime.Now;
			}
		}
	}
}