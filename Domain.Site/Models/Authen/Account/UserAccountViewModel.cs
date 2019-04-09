using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models
{
    public class UserAccountViewModel
    {
        public int userId { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string NewLoginPwd { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public int? PwdErrorCount { get; set; }
        public bool? IsRemeberUserName { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}
