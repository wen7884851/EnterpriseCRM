using Domain.DB.Models;
using Framework.EFData;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFDbContext context)
        {
            var users = new List<User>
            {
                new User{LoginName="admin",LoginPwd="123456",IsDeleted=false,Enabled=true}
            };
            var userSet = context.Set<User>();
            if (userSet.FirstOrDefault(t => t.LoginName== "admin") == null)
            {
                userSet.AddOrUpdate(t => new { t.Id }, users.ToArray());
                context.SaveChanges();
            }
            var projectTypes = InitProjectType();
            var projectTypeSet = context.Set<ProjectType>();
            if (projectTypeSet.Count()==0)
            {
                projectTypeSet.AddOrUpdate(t => new { t.Id }, projectTypes.ToArray());
                context.SaveChanges();
                var projectFragmentSet = context.Set<ProjectFragment>();
                foreach (var type in projectTypes)
                {
                    var projectFragments = InitProjectFragment(type);
                    projectFragmentSet.AddOrUpdate(t => new { t.Id }, projectFragments.ToArray());
                    context.SaveChanges();
                }
            }         
        }

        private List<ProjectFragment> InitProjectFragment(ProjectType projectType)
        {
            var projectFragment = new List<ProjectFragment>()
            {
                new ProjectFragment
                {
                    FragmentFund=100M,
                    FragmentProportion=0.6m,
                    IsDeleted=false,
                    projectType=projectType
                },
                 new ProjectFragment
                {
                    FragmentFund=500M,
                    FragmentProportion=0.5m,
                    IsDeleted=false,
                    projectType=projectType
                },
                  new ProjectFragment
                {
                    FragmentFund=2000M,
                    FragmentProportion=0.4m,
                    IsDeleted=false,
                    projectType=projectType
                },
                   new ProjectFragment
                {
                    FragmentFund=5000M,
                    FragmentProportion=0.35m,
                    IsDeleted=false,
                    projectType=projectType
                },
                new ProjectFragment
                {
                    FragmentFund=10000M,
                    FragmentProportion=0.3m,
                    IsDeleted=false,
                    projectType=projectType
                },
                new ProjectFragment
                {
                    FragmentFund=50000M,
                    FragmentProportion=0.2m,
                    IsDeleted=false,
                    projectType=projectType
                },
            };
            return projectFragment;
        }

        private List<ProjectType> InitProjectType()
        {
            var projectTypes = new List<ProjectType>()
            {
                new ProjectType
                {
                    TypeName="预算编制",
                    CivilProportion=1M,
                    SetupProportion=1M,
                    IsDeleted=false
                },
                new ProjectType
                {
                    TypeName="预算审核",
                    CivilProportion=0.7M,
                    SetupProportion=0.7M,
                    IsDeleted=false
                },
                new ProjectType
                {
                    TypeName="结算审核",
                    CivilProportion=1,
                    SetupProportion=1,
                    IsDeleted=false
                },
                new ProjectType
                {
                    TypeName="结算编制",
                    CivilProportion=1.2M,
                    SetupProportion=1.2M,
                    IsDeleted=false
                },
                new ProjectType
                {
                    TypeName="结算编制(个人)",
                    CivilProportion=1.2M*0.4M,
                    SetupProportion=1.2M*0.4M,
                    IsDeleted=false
                }
            };

            return projectTypes;
        }
    }
}
