using Core.Common;
using Core.Common.ToolsHelper;
using Core.Common.ToolsHelper.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Domain.Site.Models.System
{
    public class CurrentUser
    {
        [Import(typeof(ICache))]
        private readonly ICache _chche;
        public CurrentUser(HttpApplication application)
        {
            var TokenMessage = CookieHelper.GetCookieValue("CurrentUser_Token").Split(',');
            if(Net.Ip== TokenMessage[1])
            {
                var user= _chche.GetCache<CurrentUser>(TokenMessage[0]);
                this.UserId = user.UserId;
                this.Token = user.Token;
                this.RoleIds = user.RoleIds;
            }
            else
            {
                application.Response.Redirect("Login");
            }
        }

        protected int UserId { get; set; }
        protected string Token { get; set; }
        protected List<int> RoleIds { get; set; }

    }




}
