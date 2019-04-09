using System;

namespace Framework.Tool.Operator
{
    public class OperatorModel
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Enabled { get; set; }
        public bool IsFirstLogin { get; set; }
        public string AppKey { get; set; }
        public int? PwdErrorCount { get; set; }
        public int? LoginCount { get; set; }
        public DateTime? RegisterTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool? IsRemeberUserName { get; set; }
        public string LoginIPAddress { get; set; }
        public string LoginIPAddressName { get; set; }
        public string Token { get; set; }
        /// <summary>
        /// 通讯SessionID
        /// </summary>
        public string SessionID { get; set; }
        /// <summary>
        /// 系统GID
        /// </summary>
        public string SystemGid { get; set; }
        public DateTime? LoginTime { get; set; }
        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSystem { get; set; }
    }
}
