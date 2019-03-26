﻿using AutoMapper;
using Core.Service.Authen;
using Domain.DB.Models;
using Domain.Repository;
using Domain.Site.Models;
using Framework.EFData.DBExtend;
using Framework.Tool.Operator;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Service
{
    [Export(typeof(IProjectManager))]
    public class ProjectManager: IProjectManager
    {
        #region 属性
        [Import]
        private IProjectRepository _projectRepository { get; set; }
        [Import]
        private IProjectPointManager _projectPointManager { get; set; }
        [Import]
        private IUserService _userService { get; set; }

        public IQueryable<Project> projects
        {
            get { return _projectRepository.Entities; }
        }

        public IQueryable<Project> GetCurrentUserProject()
        {
            var user = OperatorProvider.Provider.GetCurrent();   //OperatorProvider.Provider.GetCurrent();
            var userStoreProject = _projectPointManager.projectPoints
                .Where(t => (t.UserId == user.UserId)&&t.IsDeleted==false)
                .Select(t=>t.project);
            var projectList =projects.Where(p => p.ProjectLeader == user.UserId);
            projectList.Concat(userStoreProject);
            return projectList;
        }
        #endregion

        #region 公共方法
        public void UpdateProject(ProjectViewModel project)
        {
            var projectDTO = projects.FirstOrDefault(t => t.Id == project.Id.Value);
            if (projectDTO != null)
            {
                projectDTO.ProjectName = project.ProjectName;
                projectDTO.ProjectLeader = project.ProjectLeader;
                projectDTO.Note = project.Note;
                projectDTO.Address = project.Address;
                projectDTO.Modify();
                projectDTO.Content = project.Content;
                using (UnitOfWork tran = new UnitOfWork())
                {
                    _projectRepository.Update(projectDTO);
                    tran.Commit();
                }
            }
        }

        public void DeleteProject(int projectId)
        {
            using (UnitOfWork tran = new UnitOfWork())
            {
                _projectRepository.Delete(projectId);
                tran.Commit();
            }
        }
        public int CreateProject(ProjectViewModel project)
        {
            var projectLeaderuserId = _userService.Users.FirstOrDefault(t => (t.LoginName == project.LeaderName)
            && t.IsDeleted==false).Id;
            var projectDTO = new Project()
            {
                ProjectName = project.ProjectName,
                ProjectLeader = projectLeaderuserId,
                Content = project.Content,
                Address = project.Address,
                LinkPerson = project.LinkPerson,
                LinkPhoneNo = project.LinkPhoneNo
            };
            projectDTO.Create();
            using (UnitOfWork tran = new UnitOfWork())
            {
                _projectRepository.Insert(projectDTO);
                tran.Commit();
            }
            return projectDTO.Id;
        }

        public PageResult<ProjectViewModel> GetCurrentUserProjectViewModel(ProjectSerchModel model)
        {
            var expr = BuildSearchProject(model);
            var projectDTO = expr!=null?GetCurrentUserProject().Where(expr): GetCurrentUserProject();
            var user = _userService.Users;
            var result = new PageResult<ProjectViewModel>()
            {
                Items = projectDTO.OrderByDescending(t => t.CreateTime).Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).AsEnumerable()
                .Select(t => new ProjectViewModel()
                {
                    Id = t.Id,
                    ProjectName = t.ProjectName,
                    ProjectLeader = t.ProjectLeader,
                    LeaderName = user.FirstOrDefault(u => u.Id == t.ProjectLeader.Value).LoginName,
                    LinkPerson = t.LinkPerson,
                    LinkPhoneNo = t.LinkPhoneNo,
                    Content = t.Content,
                    Address = t.Address,
                    Note = t.Note,
                    CreateTime = t.CreateTime.HasValue ? t.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss:ms") : ""
                }).ToList(),
                TotalItemsCount = projectDTO.Count()
            };
            return result;
        }

        public decimal GetUserProjectSouceByUserId(int UserId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 私有方法
        private Expression<Func<Project, bool>> BuildSearchProject(ProjectSerchModel model)
        {
            var bulider = new DynamicLambda<Project>();
            Expression<Func<Project, bool>> expr = t =>t.IsDeleted==false;
            if (!string.IsNullOrEmpty(model.projectName))
            {
                Expression<Func<Project, bool>> tmp = t => t.ProjectName.Contains(model.projectName.Trim());
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            if (model.projectId!=null)
            {
                Expression<Func<Project, bool>> tmp = t => t.Id== model.projectId;
                expr = bulider.BuildQueryAnd(expr, tmp);
            }
            return expr;
        }
        #endregion
    }
}