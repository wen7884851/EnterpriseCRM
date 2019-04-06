using Domain.DB.Models;
using Domain.Repository;
using Domain.Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Framework.EFData.DBExtend;
using System.Linq.Expressions;
using Core.Service.Authen;

namespace Core.Service.ProjectManager.Impl
{
    [Export(typeof(IProjectUserStoreManager))]
    public class ProjectUserStoreManager: IProjectUserStoreManager
    {
        #region 属性
        [Import]
        private IProjectUserStoreRepository _projectUserStoreRepository { get; set; }
        [Import]
        private IProjectPointManager _projectPointManager { get; set; }
        [Import]
        private IUserService _userService { get; set; }

        public IQueryable<ProjectPointUserStore> projectPointUserStores
        {
            get { return _projectUserStoreRepository.NoCahecEntities; }
        }
        #endregion

        public int[] GetUserStoreUserIdsByPointId(int pointId)
        {
            var userStoreList = projectPointUserStores.Where(t => t.ProjectPointId == pointId && t.IsDeleted == false && t.UserId != null);
            if ((userStoreList != null) && userStoreList.Any())
            {
                return userStoreList.Select(t => t.UserId.Value).AsEnumerable().ToArray();
            }
            return null;
        }

        public IEnumerable<ProjectUserStoreViewModel> GetUserStoreListByPointId(int pointId)
        {
            var userStoreList = Mapper.Map<List<ProjectUserStoreViewModel>>(projectPointUserStores.Where(t => t.ProjectPointId == pointId).ToList());
            foreach(var userStore in userStoreList)
            {
                userStore.UserName = _userService.Users.FirstOrDefault(t => t.Id == userStore.UserId).LoginName;
            }
            return userStoreList;
        }


        public ActionResultViewModel CreateProjectUserStore(ProjectUserStoreViewModel model)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = false
            };
            if ((model != null) && model.ProjectPointId != null)
            {
                var restProportion = _projectPointManager.GetPointRestProportion(model.ProjectPointId.Value) - model.UserProportion;
                if (restProportion >= 0)
                {
                    var projectUserStoreDTO = Mapper.Map<ProjectPointUserStore>(model);
                    projectUserStoreDTO.Create();
                    _projectUserStoreRepository.Insert(projectUserStoreDTO);
                    result.Result = restProportion;
                    result.IsSuccess = true;
                }
                else
                {
                    result.Result = "已超出项目分项提成占比" + (-restProportion).ToString() + "%";
                }
            }
            else
            {
                result.Result = "数据有误，请查证！";
            }
            return result;
        }

        public ProjectUserStoreViewModel GetUserStoreById(int storeId)
        {
            var store = projectPointUserStores.FirstOrDefault(t => t.Id == storeId);
            var result = Mapper.Map<ProjectUserStoreViewModel>(store);
            result.UserName = _userService.Users.FirstOrDefault(t => t.Id == result.UserId).LoginName;
            return result;
        }

        public ActionResultViewModel DeleteUserStoreById(int storeId)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = false
            };
            if (storeId>0)
            {
                using (UnitOfWork tran = new UnitOfWork())
                {
                    storeId = _projectUserStoreRepository.Delete(storeId);
                    tran.Commit();
                }
                result.IsSuccess = true;
            }
            else
            {
                result.Result = "Id不存在或为0，请查证数据";
            }
            return result;
        }

        public ActionResultViewModel UpdateProjectUserStore(ProjectUserStoreViewModel model)
        {
            var result = new ActionResultViewModel()
            {
                IsSuccess = false
            };
            if (model.Id>0)
            {
                var projectUserStoreDTO = _projectUserStoreRepository.Entities.FirstOrDefault(t=>t.Id==model.Id);
                UpdateStoreModel(model, projectUserStoreDTO);
                using (UnitOfWork tran = new UnitOfWork())
                {
                     _projectUserStoreRepository.Update(projectUserStoreDTO);
                    tran.Commit();
                }
                result.IsSuccess = true;
            }
            else
            {
                result.Result = "数据更新异常";
            }
            return result;
        }

        private void UpdateStoreModel(ProjectUserStoreViewModel model, ProjectPointUserStore entity)
        {
            entity.StoreContent = model.StoreContent;
            if((model.UserId!=null)&&model.UserId>0)
            {
                entity.UserId = model.UserId;
            }
        }

        public int DeleteProjectUserStore(int userStoreId)
        {
            int storeId = 0;
            if (userStoreId>0)
            {
                var projectUserStoreDTO = projectPointUserStores.FirstOrDefault(t => t.Id == userStoreId);
                projectUserStoreDTO.IsDeleted = true;
                using (UnitOfWork tran = new UnitOfWork())
                {
                    storeId = _projectUserStoreRepository.Update(projectUserStoreDTO);
                }
            }
            return storeId;
        }

        public PageResult<ProjectUserStoreViewModel> GetUserStoreListByQuery(ProjectUserStoreQueryModel queryModel)
        {
            var expr = BuildSearchUserStore(queryModel);
            var userStoreDTO = projectPointUserStores.Where(expr).AsEnumerable();
            var Items = userStoreDTO.Select(t => new ProjectUserStoreViewModel()
            {
                Id = t.Id,
                UserId=t.UserId,
                UserName= _userService.Users.FirstOrDefault(m=>m.Id==t.UserId).LoginName,
                StoreContent=t.StoreContent,
                DeleteItem=true,
                CreateTime = t.CreateTime.Value.ToLocalTime().ToString()
            }).OrderByDescending(t => t.CreateTime).Skip((queryModel.PageIndex-1)*queryModel.PageSize).Take(queryModel.PageSize).ToList();

            var result = new PageResult<ProjectUserStoreViewModel>()
            {
                Items = Items,
                TotalItemsCount = Items.Count()
            };
            return result;
        }

        private Expression<Func<ProjectPointUserStore, bool>> BuildSearchUserStore(ProjectUserStoreQueryModel model)
        {
            var bulider = new DynamicLambda<ProjectPointUserStore>();
            Expression<Func<ProjectPointUserStore, bool>> expr = t => t.IsDeleted == false;
            Expression<Func<ProjectPointUserStore, bool>> tmp;
            if (model.PointId.HasValue)
            {
                tmp = t => t.ProjectPointId == model.PointId;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (model.UserId.HasValue)
            {
                tmp = t => t.UserId == model.UserId;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            return expr;
        }
    }
    
}
