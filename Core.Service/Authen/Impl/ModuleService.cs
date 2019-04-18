using AutoMapper;
using Core.Repository.Authen;
using Domain.DB.Models;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
                var moduleDTO = Mapper.Map<Module>(model);
                DefultOrderSort(model);
                DefultIcon(model);
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

        private void DefultIcon(ModuleViewModel model)
        {
            if (model.Icon == "")
            {

            }
        }

        private void DefultOrderSort(ModuleViewModel model)
        {
            if (model.OrderSort == 0)
            {
                var last = Modules.Where(t => (t.IsDeleted == false) && t.Layer == model.Layer).OrderByDescending(t => t.OrderSort).FirstOrDefault();
                model. last?.OrderSort
            }
        }
    }
}
