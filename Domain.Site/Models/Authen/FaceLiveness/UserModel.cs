using Framework.Tool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Authen.FaceLiveness
{
    public class UserModel : EntityCommon
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IsActivated { get; set; }
        public string RegisteredStartdate { get; set; }
        public string RegisteredEnddate { get; set; }
    }
}
