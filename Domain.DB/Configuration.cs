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
