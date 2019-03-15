using Domain.DB.Models;
using Domain.Repository;
using Domain.Site.Models;
using Framework.EFData.DBExtend;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Common;

namespace Core.Service
{
    public class FormulaManager: IFormulaManager
    {
        public IQueryable<Formula> formulas
        {
            get { return _formulaRepository.NoCahecEntities; }
        }
        private IQueryable<FormulaItem> formulaItems
        {
            get { return _formulaItemRepository.NoCahecEntities; }
        }
        #region 属性
        [Import]
        private IFormulaRepository _formulaRepository { get; set; }
        [Import]
        private IFormulaItemRepository _formulaItemRepository { get; set; }
        [Import]
        private IProjectPointManager _projectPointManager { get; set; }

        public int CreateFormula(Formula formulaDTO)
        {
            formulaDTO.Create();
            using (UnitOfWork tran = new UnitOfWork())
            {
                _formulaRepository.Insert(formulaDTO);
                tran.Commit();
            }
            return formulaDTO.Id;
        }

        public int CreateFormulaItem(FormulaItemViewModel formulaItem)
        {
            var formulaItemDTO = new FormulaItem()
            {
                FormulaId = formulaItem.FormulaId,
                ProjectPointId = formulaItem.ProjectPointId,
                ItemName = formulaItem.ItemName,
                Value = formulaItem.Value
            };
            using (UnitOfWork tran = new UnitOfWork())
            {
                formulaItemDTO.Create();
                _formulaItemRepository.Insert(formulaItemDTO);
                tran.Commit();
            }
            return formulaItemDTO.Id;
        }

        public int DeleteFormulaItem(List<int> formulaItemIds)
        {
            var entityList = formulaItems.Where(t => formulaItemIds.Contains(t.Id)).ToList();
            entityList.ForEach(t => t.IsDeleted = false);
            entityList.ForEach(t => _formulaItemRepository.Update(t));
            return formulaItemIds.Count();
        }

        public int UpdateFormula(Formula formulaDTO)
        {
            formulaDTO.Modify();
            using (UnitOfWork tran = new UnitOfWork())
            {
                _formulaRepository.Update(formulaDTO);
                tran.Commit();
            }
            return formulaDTO.Id;
        }

        public List<string> GetFormulaItems(int formulaId)
        {
            var calculateFormula = formulas.FirstOrDefault(t => t.Id == formulaId).FormulaContent;
            return ExtractFormulaItems(calculateFormula);
        }

        

        public decimal CalculateCommission(List<FormulaItemViewModel> formulaItemList)
        {
            decimal result = 0;
            if (formulaItemList.Any())
            {
                var formulaId = _projectPointManager.projectPoints.FirstOrDefault(t => t.Id == formulaItemList[0].ProjectPointId).FormulaId;
                var calculateFormula = formulas.FirstOrDefault(t => t.Id == formulaId).FormulaContent;
                var FormulaContentItems = GetFormulaItems(formulaId);
                var itemsExcept = formulaItemList.Select(t => t.ItemName).Except(FormulaContentItems);
                if(itemsExcept.Any())
                {
                    foreach(var item in itemsExcept)
                    {
                        formulaItemList.Add(new FormulaItemViewModel()
                        {
                            ItemName= item,
                            Value=0
                        });
                    }
                }
                string finallyCalculateFormula = ReplaceFormulaContent(formulaItemList, calculateFormula);
                result = CalculateFormula.Calculate(finallyCalculateFormula,out bool isSuccess);
            }
            return result;
        }

        private List<string> ExtractFormulaItems(string formulaContent)
        {
            var result = new List<string>();
            string item = "";
            bool isStart = false;
            for(var i=0;i< formulaContent.Length;i++)
            {
                if(formulaContent[i]=='[')
                {
                    isStart = true;
                    continue;
                }
                if (formulaContent[i] == ']')
                {
                    isStart = false;
                    result.Add(item);
                    continue;
                }
                if(isStart)
                {
                    item += formulaContent[i];
                }
            }
            return result;
        }

        private string ReplaceFormulaContent(List<FormulaItemViewModel> formulaItemList,string calculateFormula)
        {
            foreach(var item in formulaItemList)
            {
                string itemString = $"[{item.ItemName}]";
                calculateFormula.Replace(itemString, item.Value.ToString());
            }
            return calculateFormula;
        }
        #endregion


    }
}
