//**********************************
//*ClassName:SystemEnum
//*Version:1.0.0
//*Date:2017.10.27
//*Author:文闻
//*Effect:系统枚举
//**********************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DB.Enum
{
    public enum ResourcesType
    {
        Menu=1,
        Filter= 2,
        Chart=3,
        Action=4,

    }

    public enum ResourcesEnum
    {
        // Menu
        Dashboard = 1,
        AttendanceManagement=2,
        StaffManagement=3,
        ProjectManagement=4,
        FinancialManagement=5,

        //Filter
        Datetime=11,
        Project=12,

        //Chart
        Attendance=21,
        Financial=22,

    }
}
