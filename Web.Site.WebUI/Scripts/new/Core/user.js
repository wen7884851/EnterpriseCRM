var query = {};
var actionUrl = {
    GetUserListByQuery: "/Core/User/GetUserListByQuery", GetRoleList: "/Core/Role/GetAllRole",
    CreateUser: "/Core/User/CreateUser", ResetPassWord: "/Core/User/ChangePwd", GetUserById: "/Core/User/GetUserProfile",
    DeleteUser: "/Core/User/DeleteUser", GetUserAccountById: "/Core/User/GetUserAccountById"};

var aoColumns = [
    {
        "sName": "LoginName",
        "fnRender": function (oObj) {
            return '<a href="#" onclick="OpenEditUserModal(' + oObj.Id + ')">' + oObj.LoginName + '</a>';
        }
    },
    { "sName": "FullName" },
    { "sName": "Email" },
    { "sName": "Phone" },
    {
        "sName": "LastLoginTime",
        "fnRender": function (oObj) {
            if (!oObj.LastLoginTime || oObj.LastLoginTime === null) {
                return "新建未登录";
            }
            let date = new Date(parseInt(oObj.LastLoginTime.replace("/Date(", "").replace(")/", ""), 10));
            let time_str = date.toLocaleDateString()+"  " + date.toLocaleTimeString();
            return time_str;
        }
    },
    { "sName": "RoleName" },
    {
        "sName": "Id",
        "fnRender": function (oObj) {
            let btnArray = '<a class="btn btn-xs btn-default" href="#!" title="重置密码" data-toggle="tooltip" style="margin-left:20px"><i class="mdi mdi-lock-reset" onclick="OpenResetPasswordModal(' + oObj.Id + ')"></i></a>';
            btnArray += '<a class="btn btn-xs btn-default" href="#!" title="删除" data-toggle="tooltip"><i class="mdi mdi-delete" onclick="OpenDeleteUserModal(' + oObj.Id + ')"></i></a>';
            return btnArray;
        }
    }
];

$(function () {
    Search(1);
});

function Search(index) {
    if (index && index <= 0) {
        index = 1;
    }
    query['PageIndex'] = index;
    query['LoginName'] = $("#SLoginName").val() ? $("#SLoginName").val() : '';
    query['FullName'] = $("#SFullName").val() ? $("#SFullName").val() : '';
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.GetUserListByQuery, aoColumns, query);
}

function OpenCreateUserModal() {
    lightyear.loading('show');
    $.ajax({
        type: 'GET',
        url: actionUrl.GetRoleList,
        async: false,
        success: function (result) {
            if (result && result.length > 0) {
                let roleHtml = '<option value="0" selected>请选择</option>';
                result.forEach(i => {
                    roleHtml += '<option value="' + i.value + '">' + i.text + '</option>';
                });
                $("#RoleId").html(roleHtml);
                initCreateUserErrorMsg();
                initCreateUserHtml();
                $('#createUserModal').modal('show');
            }
            else {
                setTimeout(function () { lightyear.notify('无用户数据，请先配置用户', 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}
function CreateUser() {
    if (CheckCreateUser()) {
        lightyear.loading('show');
        let user = GetCreateUserHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.CreateUser,
            data: user,
            async: false,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        setTimeout(function () { lightyear.notify('创建成功', 'success'); }, 1e3);
                        $('#createUserModal').modal('hide');
                        $('#searchBtn').click();
                    }
                    else {
                        setTimeout(function () { lightyear.notify(GetErrorMsg(result), 'danger'); }, 1e3);
                    }
                }
                else {
                    setTimeout(function () { lightyear.notify('系统错误，请联系管理人员', 'danger'); }, 1e3);
                }
                lightyear.loading('hide');
            }
        });
    }
    else {
        setTimeout(function () { lightyear.notify('请填写完整信息！', 'warning'); }, 1e3);
    }
}
function CheckCreateUser() {
    initCreateUserErrorMsg();
    let isCheck = true;
    if ($("#LoginName").val() === "") {
        $("#LoginNameErrorMsg").html('用户名不为空');
        isCheck = false;
    }
    if ($("#LoginPassword").val() === "") {
        $("#LoginPasswordErrorMsg").html('密码不为空');
        isCheck = false;
    }
    if ($("#LoginPassword").val().length < 6 ) {
        $("#LoginPasswordErrorMsg").html('密码能小于6位');
        isCheck = false;
    }
    let selectRole = $("#RoleId");
    let roleId = parseInt(selectRole[0].options[selectRole[0].selectedIndex].value);
    if (roleId < 1) {
        $("#RoleIdErrorMsg").html('请选择用户角色');
        isCheck = false;
    }
    if ($("#FullName").val() === "") {
        $("#FullNameErrorMsg").html('用户姓名不为空');
        isCheck = false;
    }
    if ($('#Phone').val() !== "" &&!checkPoneAvailable($('#Phone').val())) {
        isCheck = false;
        $('#PhoneErrorMsg').html('联系电话不符合规则');
    }
    if ($('#Email').val() !== "" && !checkEmail($('#Email').val())) {
        isCheck = false;
        $('#EmailMsg').html('邮箱地址不符合规则');
    }
    return isCheck;
}
function GetCreateUserHtmlValue() {
    let selectRole = $("#RoleId");
    let roleId = parseInt(selectRole[0].options[selectRole[0].selectedIndex].value);
    return {
        FullName: $("#FullName").val(), Phone: $('#Phone').val(), Email: $('#Email').val(), LoginName: $("#LoginName").val(),
        LoginPwd: $("#LoginPassword").val(), RoleId: roleId, UserId: 0
    };
}
function initCreateUserErrorMsg() {
    $("#LoginNameErrorMsg").html('');
    $("#LoginPasswordErrorMsg").html('');
    $("#RoleIdErrorMsg").html('');
    $("#FullNameErrorMsg").html('');
    $('#PhoneErrorMsg').html('');
    $('#EmailMsg').html('');
}
function initCreateUserHtml() {
    $("#LoginName").val('');
    $("#LoginPassword").val('');
    $("#FullName").val('');
    $("#Phone").val('');
    $("#Email").val('');
}

function OpenResetPasswordModal(userId) {
    initResetPassWordHtml();
    initResetPassWordErrorMsg();
    $('#ResetPasswordModal').modal('show');
}
function ResetPassWord() {
    if (CheckResetPassWord()) {
        lightyear.loading('show');
        let user = GetResetPassWordHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.ResetPassWord,
            data: user,
            async: false,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        setTimeout(function () { lightyear.notify('密码重置成功！', 'success'); }, 1e3);
                        $('#ResetPasswordModal').modal('hide');
                        $('#searchBtn').click();
                    }
                    else {
                        setTimeout(function () { lightyear.notify(GetErrorMsg(result), 'danger'); }, 1e3);
                    }
                }
                else {
                    setTimeout(function () { lightyear.notify('系统错误，请联系管理人员', 'danger'); }, 1e3);
                }
                lightyear.loading('hide');
            }
        });
    }
}
function GetResetPassWordHtmlValue() {
    return { UserId: $('#Reset_UserId').val(), LoginPwd: $('#RLoginPassword').val() };
}
function CheckResetPassWord() {
    initResetPassWordErrorMsg();
    let isCheck = true;
    let password = $('#RLoginPassword').val();
    let checkpassword = $('#RNewLoginPassword').val();
    if (password === '' || password !== checkpassword) {
        $('#RLoginPasswordErrorMsg').html('密码为空或两次密码不一致,请重输！');
        isCheck = false;
    }
    return isCheck;
}
function initResetPassWordErrorMsg() {
    $('#RLoginPasswordErrorMsg').html('');
}
function initResetPassWordHtml() {
    $('#Reset_UserId').val(userId);
    $('#RLoginPassword').val('');
    $('#RNewLoginPassword').val('');
}

function OpenDeleteUserModal(userId) {
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: actionUrl.GetUserById,
        data: { userId: userId },
        async: false,
        success: function (result) {
            if (result) {
                $('#DuserId').val(userId);
                $('#DUserName').html(result.FullName);
                $('#DeleteUserModal').modal('show');
            }
            else {
                setTimeout(function () { lightyear.notify('系统错误，请联系管理人员', 'danger'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}
function DeleteUser() {
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: actionUrl.DeleteUser,
        data: { userId: $('#DuserId').val() },
        async: false,
        success: function (result) {
            if (result) {
                if (result.IsSuccess) {
                    setTimeout(function () { lightyear.notify('删除用户成功！', 'success'); }, 1e3);
                    $('#DeleteUserModal').modal('hide');
                    $('#searchBtn').click();
                }
                else {
                    setTimeout(function () { lightyear.notify(GetErrorMsg(result), 'danger'); }, 1e3);
                }
            }
            else {
                setTimeout(function () { lightyear.notify('系统错误，请联系管理人员', 'danger'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}


function OpenEditUserModal(userId) {
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: actionUrl.GetUserAccountById,
        data: { userId: userId},
        async: false,
        success: function (result) {
            if (result.UserId) {
                initEditUserHtml(result);
            }
            else {
                setTimeout(function () { lightyear.notify(GetErrorMsg(result), 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}
function initEditUserHtml(userAccount) {
    $('#EUserId').val(userAccount.UserId);
    $('#ELoginName').val(userAccount.LoginName);
    $('#EFullName').val(userAccount.FullName);
    $('#EPhone').val(userAccount.Phone);
    $('#EEmail').val(userAccount.Email);
    $.ajax({
        type: 'GET',
        url: actionUrl.GetRoleList,
        async: false,
        success: function (result) {
            if (result[0].value) {
                let roleHtml = '<option value="0">请选择</option>';
                result.forEach(i => {
                    if (i.value === userAccount.RoleId) {
                        roleHtml += '<option value="' + i.value + '" selected>' + i.text + '</option>';
                    }
                    else {
                        roleHtml += '<option value="' + i.value + '">' + i.text + '</option>';
                    }
                });
                $('#ERoleId').html(roleHtml);
                initEditUserErrorMsg();
                $('#editUserModal').modal('show');
            }
            else {
                setTimeout(function () { lightyear.notify(GetErrorMsg(result), 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}
function initEditUserErrorMsg() {
    $('#ELoginNameErrorMsg').html('');
    $('#ERoleIdErrorMsg').html('');
    $('#EFullNameErrorMsg').html('');
    $('#EPhoneErrorMsg').html('');
    $('#EEmailMsg').html('');
}