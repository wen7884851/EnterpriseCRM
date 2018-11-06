using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.Authen.FaceLiveness
{
    public class ExportModel
    {
        public string RealName { get; set; }
        public string IDCard { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string OrgName { get; set; }
    }
}
