//**********************************
//*ClassName: EF数据访问上下文
//*Version:1.0.0
//*Date:2017.10.09
//*Author:
//*Effect:
//**********************************

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Core.EFData
{
    /// <summary>
    ///     EF数据访问上下文
    /// </summary>
    [Export("EF", typeof(DbContext))]
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("default")
        { }

        public EFDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public EFDbContext(DbConnection existingConnection)
            : base(existingConnection, true)
        { }

        [ImportMany(typeof(IEntityMapper))]
        public IEnumerable<IEntityMapper> EntityMappers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //EF默认设置级联删除，先移除默认规则，需要再配置
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            if (EntityMappers == null)
            {
                return;
            }

            foreach (var mapper in EntityMappers)
            {
                mapper.RegistTo(modelBuilder.Configurations);
            }
        }
    }
}
