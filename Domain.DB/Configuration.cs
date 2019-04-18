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
            var modules = new List<Module>
            {
                new Module{ Name="系统模块",ParentId=0,Layer=1,OrderSort=1,Icon="mdi-widgets" }
            };
            var moduleSet = context.Set<Module>();
            if (moduleSet.FirstOrDefault(t => t.Name == "系统模块") == null)
            {
                moduleSet.AddOrUpdate(t => new { t.Id }, modules.ToArray());
                context.SaveChanges();
            }
            var roles = new List<Role>
            {
                new Role{ Name="系统管理员" }
            };
            var roleSet = context.Set<Role>();
            if(roleSet.FirstOrDefault(t => t.Name == "系统管理员") == null)
            {
                roleSet.AddOrUpdate(t => new { t.Id }, roles.ToArray());
                context.SaveChanges();
            }
            var users = new List<User>
            {
                new User{LoginName="admin",RoleId=roles[0].Id,LoginPwd="123456",IsDeleted=false,Enabled=true,isFirstLogin=true}
            };
            var userSet = context.Set<User>();
            if (userSet.FirstOrDefault(t => t.LoginName== "admin") == null)
            {
                userSet.AddOrUpdate(t => new { t.Id }, users.ToArray());
                context.SaveChanges();
                var profile = new List<UserProfile>
                {
                    new UserProfile{FullName="系统管理员",UserId=userSet.FirstOrDefault(t => t.LoginName== "admin").Id}
                 };
                var profileSet = context.Set<UserProfile>();
                profileSet.AddOrUpdate(t => new { t.Id }, profile.ToArray());
                context.SaveChanges();
            }
            var projectTypes = new List<PointProfessionalType>
            {
                new PointProfessionalType{TypeName="土建"},
                new PointProfessionalType{TypeName="安装"},
                new PointProfessionalType{TypeName="市政"},
                new PointProfessionalType{TypeName="道路"}
            };
            var pointProfessionalTypeSet = context.Set<PointProfessionalType>();
            if (pointProfessionalTypeSet.FirstOrDefault(t => t.TypeName == "土建") == null)
            {
                pointProfessionalTypeSet.AddOrUpdate(t => new { t.Id }, projectTypes.ToArray());
                context.SaveChanges();
            }
        }
    }
}
