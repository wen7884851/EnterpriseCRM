using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Tool
{
    public class FunConfigClass
    {
        /// <summary>
        /// 业务代码
        /// </summary>
        public string FunCode { get; set; }
        /// <summary>
        /// 业务描述
        /// </summary>
        public string FunDescription { get; set; }

        /// <summary>
        /// 服务类型
        /// 1、HIS服务 2、银行服务 3 医保服务 4 预交金平台服务
        /// </summary>
        public string ServiceCode { get; set; }
        /// <summary>
        /// 通讯方式
        ///  01 为WebService通讯 02 为Socket通讯
        /// </summary>
        public string ProxyCode { get; set; }
        /// <summary>
        /// 通讯地址
        /// </summary>
        public string AccessUrl { get; set; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string FunName { get; set; }
        /// <summary>
        /// 通讯超时时间
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// 协议前缀长度
        /// </summary>
        public int PrefixLen { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Encode { get; set; }


        private int _BufSize = 1024 * 10;
        /// <summary>
        /// 每次接收缓存大小
        /// </summary>
        public int BufSize
        {
            get { return _BufSize; }
            set { _BufSize = value; }
        }
    }
}
