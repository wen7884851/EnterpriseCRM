using Domain.DB.Models;
using Framework.EFData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var moduleSet = context.Set<Module>();
            if (moduleSet.FirstOrDefault(t => t.Name == "系统模块") == null)
            {
                InitModules(context);
            }
            var roles = new List<Role>
            {
                new Role{ Name="系统管理员",Enabled=true,IsDeleted=false},
                new Role{ Name="普通成员",Enabled=true,IsDeleted=false }
            };
            var roleSet = context.Set<Role>();
            if(roleSet.FirstOrDefault(t => t.Name == "系统管理员") == null)
            {
                roleSet.AddOrUpdate(t => new { t.Id }, roles.ToArray());
                context.SaveChanges();
            }
            var roleModules = new List<RoleModule>();
            var moduleIds = moduleSet.Select(t => t.Id);
            var roleId = roleSet.FirstOrDefault().Id;
            foreach (var moduleId in moduleIds)
            {
                var roleModule = new RoleModule
                {
                    ModuleId = moduleId,
                    RoleId = roleId,
                    IsSearch = true,
                    IsEdit = true,
                    IsCreate = true,
                    IsDeleted = false,
                    CreateTime=DateTime.Now
                };
                roleModules.Add(roleModule);
            }
            var roleModuleSet = context.Set<RoleModule>();
            if(roleModuleSet.FirstOrDefault()==null)
            {
                roleModuleSet.AddOrUpdate(t => new { t.Id }, roleModules.ToArray());
                context.SaveChanges();
            }
            var users = new List<User>
            {
                new User{LoginName="admin",RoleId=roleId,LoginPwd="123456",IsDeleted=false,Enabled=true,isFirstLogin=true}
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

        private void InitModules(EFDbContext context)
        {
            var moduleSet = context.Set<Module>();
            var parentModules = new List<Module>
            {
                new Module{ Name="系统模块",ParentId=0,Layer=1,OrderSort=1,Icon="mdi mdi-cloud",Enabled=true,IsMenu=true },
                new Module{ Name="项目模块",ParentId=0,Layer=1,OrderSort=2,Icon="mdi mdi-widgets",Enabled=true,IsMenu=true },
                new Module{ Name="申报模块",ParentId=0,Layer=1,OrderSort=3,Icon="mdi mdi-send",Enabled=true,IsMenu=true },
                new Module{ Name="财务模块",ParentId=0,Layer=1,OrderSort=4,Icon="mdi mdi-skype",Enabled=true,IsMenu=true },
                new Module{ Name="报表模块",ParentId=0,Layer=1,OrderSort=5,Icon="mdi mdi-chart-bar",LinkUrl="/Common/Error/Page404",Enabled=true,IsMenu=true },
                new Module{ Name="模块管理",ParentId=0,Layer=1,OrderSort=6,Icon="mdi mdi-message-bulleted",LinkUrl="/Common/Error/Page404",Enabled=true,IsMenu=true }
            };
            moduleSet.AddOrUpdate(t => new { t.Id }, parentModules.ToArray());
            context.SaveChanges();

            foreach(var module in parentModules)
            {
                var childModule = new List<Module>();
                switch (module.Name)
                {
                    case "系统模块":
                        childModule.Add(new Module { Name = "模块管理", ParentId = module.Id, LinkUrl= "/Core/Module/Index"
                            , Layer = 2, OrderSort = 1, Icon = "mdi mdi-view-list", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "角色管理", ParentId = module.Id, LinkUrl= "/Core/Role/Index"
                            , Layer = 2, OrderSort = 2, Icon = "mdi mdi-account-card-details", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "用户管理", ParentId = module.Id, LinkUrl= "/Core/User/Index"
                            , Layer = 2, OrderSort = 3, Icon = "mdi mdi-account-multiple-plus", Enabled = true, IsMenu = true });
                        break;
                    case "项目模块":
                        childModule.Add(new Module { Name = "项目管理", ParentId = module.Id, LinkUrl= "/Project/ProjectManager/Index"
                            , Layer = 2, OrderSort = 1, Icon = "mdi mdi-shopping", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "系数配置", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 2, Icon = "mdi mdi-tune", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "公式配置", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 3, Icon = "mdi mdi-tune-vertical", Enabled = true, IsMenu = true });
                        break;
                    case "申报模块":
                        childModule.Add(new Module { Name = "考勤记录", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 1, Icon = "mdi mdi-book", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "请假申请", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 2, Icon = "mdi mdi-walk", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "加班申请", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 3, Icon = "mdi mdi-upload-network", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "物资申请", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 4, Icon = "mdi mdi-cart", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "差旅申请", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 5, Icon = "mdi mdi-car-sports", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "项目申报", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 6, Icon = "mdi mdi-tab-plus", Enabled = true, IsMenu = true });
                        break;
                    case "财务模块":
                        childModule.Add(new Module { Name = "物资列表", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 1, Icon = "mdi mdimdi-bulletin-board", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "个人支出列表", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 2, Icon = "mdi mdi-square-inc-cash", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "个人提成列表", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                            , Layer = 2, OrderSort = 3, Icon = "mdi mdi-currency-jpy", Enabled = true, IsMenu = true });
                        childModule.Add(new Module { Name = "项目财报", ParentId = module.Id, LinkUrl= "/Common/Error/Page404"
                             , Layer = 2, OrderSort = 3, Icon = "mdi mdi-file-chart", Enabled = true, IsMenu = true });
                        break;
                }
                if(childModule.Any())
                {
                    moduleSet.AddOrUpdate(t => new { t.Id }, childModule.ToArray());
                    context.SaveChanges();
                }
            }
        }
    }
}
