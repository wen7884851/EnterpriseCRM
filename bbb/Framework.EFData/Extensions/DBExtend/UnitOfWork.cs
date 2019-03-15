using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;//System.Transactions 引用

namespace Framework.EFData.DBExtend
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransactionScope trans = null;

        // 设置事务超时时间为60秒
        public UnitOfWork()
        { 
            trans = new TransactionScope(TransactionScopeOption.Required,new TimeSpan(0,0,30));
        }

        public void Commit()
        {
            if (trans != null)
            {
                trans.Complete();//必须要调用scope.Complete()才能将数据更新到数据库
            }
        }

        public void Dispose()
        {
            if (trans != null)
            {
                trans.Dispose();
            }
        }
    }
}
