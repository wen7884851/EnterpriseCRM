using Core.Service.Authen;
using Domain.Site.Models;
using Framework.Common.FileHelper;
using Framework.Tool.Operator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Web.Site.WebUI.Areas.System.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserController : Controller
    {
        [Import]
        private IUserService _userService { get; set; }
        // GET: System/User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadUserPhoto()
        {
            var user = OperatorProvider.Provider.GetCurrent();
            HttpPostedFileBase fostFile = Request.Files[0];
            string fileName = user.UserId.ToString() + ".jpg";
            var s= ImageThumbnailMake.ToImageAndSaveFlieByName(fostFile, fileName);
            return Json("");
        }
    }
}