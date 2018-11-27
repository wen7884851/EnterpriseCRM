using AutoMapper;
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

namespace Business.Service.Impl
{
    public class ProjectManager: IProjectManager
    {
        #region 属性
        [Import]
        public IProjectRepository _projectRepository { get; set; }
        public IQueryable<Project> projects
        {
            get { return _projectRepository.NoCahecEntities; }
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
            var projectDTO = Mapper.Map<Project>(project);
            using (UnitOfWork tran = new UnitOfWork())
            {
                _projectRepository.Insert(projectDTO);
                tran.Commit();
            }
            return projectDTO.Id;
        }

        public decimal GetUserProjectSouceByUserId(int UserId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
