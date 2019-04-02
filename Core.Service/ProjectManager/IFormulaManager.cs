using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IFormulaManager
    {
        IQueryable<Formula> formulas { get; }
        int CreateFormula(Formula formulaDTO);
        int CreateFormulaItem(FormulaItemViewModel formulaItem);
        int UpdateFormula(Formula formulaDTO);
    }
}
