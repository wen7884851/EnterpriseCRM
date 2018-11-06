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
    /// <summary>
    /// 对象类型
    /// </summary>
    public enum ResourcesType
    {
        /// <summary>
        /// 菜单/按钮权限与角色关系
        /// </summary>
        PermissionForRole = 0,
        /// <summary>
        /// 菜单/按钮权限与用户关系
        /// </summary>
        PermissionForUser = 1,
        /// <summary>
        /// 系统与角色关系
        /// </summary>
        SystemForRole = 2,
        /// <summary>
        /// 系统与用户关系
        /// </summary>
        SystemForUser = 3,
        /// <summary>
        /// 子系统用户与主系统用户关系
        /// </summary>
        SubUserForMainUser = 4,
        /// <summary>
        /// 机构与用户关系
        /// </summary>
        OrgUser = 5
    }

    /// <summary>
    /// API接口编码表
    /// </summary>

    public enum ApiActionType
    {
        /// <summary>
        /// 登录
        /// </summary>
        InLogin = 1001,
        /// <summary>
        /// 登出
        /// </summary>
        OutLogin = 1002,
        /// <summary>
        /// 检测Session有效性
        /// </summary>
        CheckSession = 1003,
        /// <summary>
        /// 检测权限有效性
        /// </summary>
        CheckPermission = 1004
    }

    /// <summary>
    /// API接口编码表
    /// </summary>

    public enum ApiFaceUpActionType
    {
        /// <summary>
        /// 获取访问令牌
        /// </summary>
        GetToken = 100,
        /// <summary>
        /// 验证访问令牌
        /// </summary>
        CheckToken = 101,
        /// <summary>
        /// 核对人员信息
        /// </summary>
        CheckPerson = 1001,
        /// <summary>
        /// 上传身份证图像
        /// </summary>
        UPIDCardPhoto = 1002,
        /// <summary>
        /// 上传人脸图像
        /// </summary>
        UPPersonPhoto = 1003,
        /// <summary>
        /// 查询身份信息，返回人员状态
        /// </summary>
        SerchRecordID = 1004,
        /// <summary>
        /// 查询地区编码
        /// </summary>
        SerchArea = 1005,
        /// <summary>
        /// 查询机构编码
        /// </summary>
        SerchOrg = 1006,
        /// <summary>
        /// 获取记录图像信息
        /// </summary>
        RecordImg = 1007,
        /// <summary>
        /// 上传身份证图像
        /// </summary>
        UPIDCardGetMsg = 1008,
        /// <summary>
        /// 上传身份证图像
        /// </summary>
        UPIDCard = 1009,
        /// <summary>
        /// 获取身份证图像上传情况
        /// </summary>
        GetImageList = 1010,

        /// <summary>
        /// 获取身份证图像上传成功的信息
        /// </summary>
        GetIDCardMsg = 1010,
        /// <summary>
        /// 上传认证头像
        /// </summary>
        UPPhoto = 1011,
        /// <summary>
        /// 获取认证头像认证结果
        /// </summary>
        GetPhotoMsg = 1012,
        /// <summary>
        /// 获取版本号
        /// </summary>
        GetVersion = 103,
        /// <summary>
        /// APP错误捕获
        /// </summary>
        ErrorCapture = 102,
        /// <summary>
        /// 获取文章信息
        /// </summary>
        GetArticle = 104,
        /// <summary>
        /// 上传APP日志记录
        /// </summary>
        DownLoadError = 105,
        /// <summary>
        /// 登录
        /// </summary>
        Login = 106,
        /// <summary>
        /// 修改密码
        /// </summary>
        ChangePassword = 107
    }
}
