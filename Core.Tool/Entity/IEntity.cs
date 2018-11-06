using Core.Tool.Operator;
using System;

namespace Core.Tool.Entity
{
    public class IEntity<TEntity>
    {
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var entity = this as ICreationAudited;
            //entity.F_Id = Common.GuId();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.CreateId = LoginInfo.UserId;
                entity.CreateBy = LoginInfo.LoginName;
            }
            entity.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify()
        {
            var entity = this as IModificationAudited;
            //entity.F_Id = keyValue;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.ModifyId = LoginInfo.UserId;
                entity.ModifyBy = LoginInfo.LoginName;
            }
            entity.ModifyTime = DateTime.Now;
        }
        public void Remove()
        {
            var entity = this as IDeleteAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.DeleteUserId = LoginInfo.UserId;
            }
            entity.DeleteTime = DateTime.Now;
            entity.DeleteMark = true;
        }
    }
}
