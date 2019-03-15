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
        private IUserService _userService { get; set; }

        public IQueryable<ProjectPointUserStore> projectPointUserStores
        {
            get { return _projectUserStoreRepository.NoCahecEntities; }
        }
        #endregion

        public int CreateProjectUserStore(ProjectUserStoreViewModel model)
        {
            var projectUserStoreDTO = Mapper.Map<ProjectPointUserStore>(model);
            projectUserStoreDTO.Create();
            int storeId = 0;
            using (UnitOfWork tran = new UnitOfWork())
            {
               storeId = _projectUserStoreRepository.Insert(projectUserStoreDTO);
            }
            return storeId;
        }

        public int UpdateProjectUserStore(ProjectUserStoreViewModel model)
        {
            int storeId = 0;
            if (model.Id>0)
            {
                var projectUserStoreDTO = Mapper.Map<ProjectPointUserStore>(model);
                using (UnitOfWork tran = new UnitOfWork())
                {
                    storeId = _projectUserStoreRepository.Update(projectUserStoreDTO);
                }
            }
            return storeId;
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
                StoreFund=t.StoreFund,
                ProjectPointProportion=t.ProjectPointProportion,
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
