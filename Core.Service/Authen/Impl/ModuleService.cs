using AutoMapper;
using Core.Repository.Authen;
using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Authen.Impl
{
    [Export(typeof(IModuleService))]
    class ModuleService : CoreServiceBase, IModuleService
    {
        #region 属性
        [Import]
        public IModuleRepository _moduleRepository { get; set; }

        public IQueryable<Module> Modules
        {
            get { return _moduleRepository.NoCahecEntities; }
        }
        #endregion

        public List<ModuleViewModel> GetAllModuleList()
        {
            var Items = Mapper.Map<List<ModuleViewModel>>(Modules.Where(t => t.IsDeleted == false));
            Items = SortModuleList(Items, 1);
            return Items;
        }

        public PageResult<ModuleViewModel> GetModuleListByQuery(ModuleQueryModel query)
        {
            var expr = BuildSearchModules(query);
            var Items = Mapper.Map<List<ModuleViewModel>>(Modules.Where(expr).ToList());
            Items = SortModuleList(Items,1);
            var result = Items.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);
            return  new PageResult<ModuleViewModel>()
            {
                Items = result,
                TotalItemsCount = Items.Count()
            };
        }

        private List<ModuleViewModel> SortModuleList(List<ModuleViewModel> moduleList,int layer)
        {
            var result =new List<ModuleViewModel>();
            var parentModuleList = moduleList.Where(t => t.Layer == layer).OrderBy(t=>t.OrderSort);
            foreach(var parentModule in parentModuleList)
            {
                result.Add(parentModule);
                if ((parentModule.LinkUrl == null)||parentModule.LinkUrl=="")
                {
                    var list = moduleList.Where(t => t.ParentId == parentModule.Id).ToList();
                    layer = parentModule.Layer + 1;
                    result.AddRange(SortModuleList(list, layer));
                }
            }
            MergeBaseModule(moduleList, result);
            return result;
        }

        private void MergeBaseModule(List<ModuleViewModel> souce, List<ModuleViewModel> merge)
        {
            var except = souce.Except(merge).OrderBy(t => t.OrderSort);
            merge.AddRange(except);
        }

        public List<OptionViewMode> GetLayerKeyValue()
        {
            var layers = Modules.Where(t => t.IsDeleted == false).Select(t=>new { t.Layer})
                .GroupBy(t => new { t.Layer }).Select(a => a.Key).OrderBy(t=>t.Layer).ToList();
            var result = new List<OptionViewMode>();
            int last = layers[layers.Count() - 1].Layer + 1;
            foreach (var layer in layers)
            {
                result.Add(new OptionViewMode
                {
                    key = layer.Layer,
                    text = layer.Layer + "  级菜单",
                    value = layer.Layer
                });
            }
            result.Add(new OptionViewMode
            {
                key = last,
                text = last + "  级菜单",
                value = last
            });
            return result;
        }

        public List<OptionViewMode> GetParentModuleKeyValue(int layer)
        {
            var parentModuleList = Modules.Where(t => t.IsDeleted == false && t.Layer == layer)
                .Select(t => new OptionViewMode {
                    key=t.Id,text=t.Name,value=t.Id
                }).OrderBy(t=>t.key);
            return parentModuleList.ToList();
        }

        public ActionResultViewModel CreateModule(ModuleViewModel model)
        {
            var result = new ActionResultViewModel() { IsSuccess = false };
            try
            {
                DefultOrderSort(model);
                DefultIcon(model);
                var moduleDTO = Mapper.Map<Module>(model);
                moduleDTO.IsMenu = true;
                moduleDTO.Enabled = true;
                moduleDTO.Create();
                _moduleRepository.Insert(moduleDTO);
                result.IsSuccess = true;
            }
            catch (Exception e)
            {
                result.Result = e.Message;
                return result;
            }
            return result;
        }

        private Expression<Func<Module, bool>> BuildSearchModules(ModuleQueryModel model)
        {
            var bulider = new DynamicLambda<Module>();
            Expression<Func<Module, bool>> expr = t => t.IsDeleted == false;
            Expression<Func<Module, bool>> tmp;
            if (model.ParentModule.HasValue&& model.ParentModule.Value>0)
            {
                tmp = t => t.ParentId == model.ParentModule;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (!string.IsNullOrEmpty(model.ModuleName))
            {
                tmp = t => t.Name.Contains(model.ModuleName);
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            return expr;
        }

        private void DefultIcon(ModuleViewModel model)
        {
            if (model.Icon == null && model.Icon == "")
            {
                model.Icon = "mdi mdi-widgets";
            }
        }

        private void DefultOrderSort(ModuleViewModel model)
        {
            if (model.OrderSort == 0)
            {
                var last = Modules.Where(t => (t.IsDeleted == false) && t.Layer == model.Layer).OrderByDescending(t => t.OrderSort).FirstOrDefault();
                model.OrderSort = last != null ? last.OrderSort : 1;
            }
        }
    }
}
