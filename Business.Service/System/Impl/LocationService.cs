using Domain.Repository.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Tool;
using Domain.DB.Models.System;
using Domain.Site.Models;
using Domain.Site.Models.AdminCommon;

namespace Business.Service.System.Impl
{
    [Export(typeof(ILocationService))]
    public class LocationService : CoreServiceBase, ILocationService
    {
        #region 属性
        [Import]
        private IAddressRepository AddressRepository { get; set; }
        [Import]
        private IAreaRepository AreaRepository { get; set; }
        [Import]
        private ICityRepository CityRepository { get; set; }

        public void AddLocations(IList<LocationViewModel> locations)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationViewModel> GetLocations(LocationQueryWhere query)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
