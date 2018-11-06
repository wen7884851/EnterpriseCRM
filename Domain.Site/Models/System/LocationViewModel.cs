using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class LocationViewModel
    {
        public int? AddressId { get; set; }
        public string AddressName { get; set; }
        public int? AreaId { get; set; }
        public string AreaName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string LocationCode { get; set; }

    }

    public class LocationQueryWhere
    {
        public LocationType type { get; set; }
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CreateUser { get; set; }
        public int? ModifyUser { get; set; }
    }

    public enum LocationType
    {
        Address=1,
        Area=2,
        City=3
    }
}
