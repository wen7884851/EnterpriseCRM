using Domain.Site.WebUI.Extension.Filter;
using System.Web;
using System.Web.Mvc;

namespace Domain.Site.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandlerErrorAttribute());
            filters.Add(new ElmahErrorAttribute());
        }
    }
}
