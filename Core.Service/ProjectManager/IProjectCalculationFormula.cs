using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProjectCalculationFormula
    {
        IQueryable<ProjectType> projectTypes { get; }
        IQueryable<ProjectFragment> projectFragments { get; }

        decimal CommonCalculationCommission(ProjectCalculationViewModel model);
    }
}
