using Domain.DB.Models;
using Domain.Repository;
using Domain.Site.Enum;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.ProjectManager.Impl
{
    [Export(typeof(IProjectCalculationFormula))]
    public class ProjectCalculationFormula: IProjectCalculationFormula
    {
        #region 属性
        [Import]
        private IProjectTypeRepository _projectTypeRepository { get; set; }
        [Import]
        private IProjectFragmentRepository _projectFragmentRepository { get; set; }
        #endregion
        public IQueryable<ProjectType> projectTypes
        {
            get { return _projectTypeRepository.NoCahecEntities; }
        }

        public IQueryable<ProjectFragment> projectFragments
        {
            get { return _projectFragmentRepository.NoCahecEntities; }
        }
        public decimal CommonCalculationCommission(ProjectCalculationViewModel model)
        {
            decimal result = 0;
            var projectType = projectTypes.FirstOrDefault(t => t.Id == model.ProjectTypeId);
            var fragments = projectFragments.Where(t=>t.ProjectTypeId== projectType.Id).OrderBy(t=>t.FragmentFund);
            foreach (var fragment in fragments)
            {
                var fund = model.PointFund - fragment.FragmentFund;
                if(fund<0)
                {
                    result += model.PointFund * fragment.FragmentProportion*100;
                    break; 
                }
                result += fragment.FragmentFund * fragment.FragmentProportion*100;
                model.PointFund = fund;
            }
            if(model.ProfessionalTypeId== (int)ProfessionalType.土建)
            {
                result = result * projectType.CivilProportion;
            }
            else
            {
                result = result * projectType.SetupProportion;
            }
            return result;
        }
    }
}
