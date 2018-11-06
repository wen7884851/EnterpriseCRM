using System.Linq;
using System.Collections.Generic;
using Domain.Site.Models;

namespace Business.Service.System
{
    public interface ILocationService
    {
        #region 属性
        IQueryable<LocationViewModel> Locations { get; }
        #endregion

        AddLocations(IList<LocationViewModel> locations);


    }
}
