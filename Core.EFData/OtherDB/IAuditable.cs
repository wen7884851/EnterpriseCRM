﻿//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.EFData.OtherDB
{
    /// <summary>
    /// 用于写数据修改，添加等历史日志
    /// </summary>
    public interface IAuditable
    {
        void WriteLog(int modelId, string userName, string moduleName, string tableName, string eventType, ModelBase newValues);
    }
}
