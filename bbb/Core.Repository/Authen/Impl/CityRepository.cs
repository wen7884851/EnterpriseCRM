using System;
using System.ComponentModel.Composition;
using System.Linq;

using Framework.EFData;
using Domain.DB.Models.Authen;


namespace Core.Repository.Authen.Impl
{
    [Export(typeof(ICityRepository))]
    public class CityRepository : EFRepositoryBase<City, Int32>, ICityRepository
    { }
}
