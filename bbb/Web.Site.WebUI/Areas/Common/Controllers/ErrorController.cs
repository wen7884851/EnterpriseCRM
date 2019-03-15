using System.Web.Mvc;
using Web.Site.WebUI.Common;

namespace Web.Site.WebUI.Areas.Common.Controllers
{
    public class ErrorController : AdminController
    {
        //
        // GET: /Common/Error/

        /// <summary>
        /// 错误页面（异常页面）
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult ErrorMessage(string message)
        {
            ViewData["Message"] = message;
            return View();
          
        }
        public ActionResult Page400()
		{
			return View();
		}

		public ActionResult Page404()
        {
            return View();
        }

		public ActionResult Page500()
		{
			return View();
		}

	}
}