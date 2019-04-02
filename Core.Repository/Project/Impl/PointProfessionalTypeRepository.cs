using Framework.EFData;
using Domain.DB.Models;
using System;
using System.ComponentModel.Composition;

namespace Domain.Repository.Impl
{
    [Export(typeof(IPointProfessionalTypeRepository))]
    public class ProjectTypeRepository : EFRepositoryBase<PointProfessionalType, int>, IPointProfessionalTypeRepository
    {
    }
}