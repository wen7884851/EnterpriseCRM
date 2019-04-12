var photoPath = "/image/main/userProfile/";
var actionUrl = {
    GetUserProfile: "/Core/User/GetUserProfile", UploadUserPhoto: "/Core/User/UploadUserPhoto",
    SaveProfile: "/Core/User/EditUserProfile", ChangePwd:"/Common/Account/ChangePwd"};
$(function () {
    var element = document.getElementById('UserPhoto');
    element.src = photoPath + "userProfile.jpg";
    init();
});

function SaveProfile() {
    if (CheckProfileView()) {
        let data = getUserProfile();
        $.ajax({
            type: 'post',
            url: actionUrl.SaveProfile,
            data: data,
            async: false,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        setTimeout(function () { lightyear.notify('保存成功！', 'success'); }, 1e3);
                    }
                    else {
                        setTimeout(function () { lightyear.notify(result.Result, 'danger'); }, 1e3);
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

function ChangePwd() {
    lightyear.loading('show');
    let oldLoginPwd = $('#oldPassword').val();
    let newLoginPwd = $('#newPassword').val();
    let newDLoginPwd = $('#newDPassword').val();
    if (newLoginPwd !== newDLoginPwd) {
        setTimeout(function () { lightyear.loading('hide'); lightyear.notify('新密码两次输入不一致', 'warning'); }, 1e3);
        return;
    }
    if (newLoginPwd.length < 6 || newLoginPwd.length > 20) {
        setTimeout(function () { lightyear.loading('hide'); lightyear.notify('新密码需要长度大于6位小于20位', 'warning'); }, 1e3);
        return;
    }
    if (newLoginPwd === oldLoginPwd) {
        setTimeout(function () { lightyear.loading('hide'); lightyear.notify('新旧密码一致', 'warning'); }, 1e3);
        return;
    }
    oldLoginPwd = hex_md5(oldLoginPwd);
    var formData = { LoginPwd: oldLoginPwd, NewLoginPwd: newLoginPwd };
    $.ajax({
        type: 'post',
        url: actionUrl.ChangePwd,
        data: formData,
        success: function (result) {
            if (result.IsSuccess) {
                $('#ChangePwdModal').modal('hide');
                setTimeout(function () { lightyear.loading('hide'); lightyear.notify('密码修改成功！', 'success'); }, 1e3);
            } else {
                setTimeout(function () { lightyear.loading('hide'); lightyear.notify(result.Result, 'danger'); }, 1e3);
            }
            $('#oldPassword').val('');
            $('#newPassword').val('');
            $('#newDPassword').val('');
            lightyear.loading('hide');
        }
    });
}

function CheckProfileView() {
    initEditProfileErrorMsg();
    let isCheck = true;
    if ($('#FullName').val() === "") {
        isCheck = false;
        $('#FullNameErrorMsg').html('姓名不能为空');
    }
    if (!checkCardNo($('#IDCardNo').val())) {
        isCheck = false;
        $('#IDCardNoErrorMsg').html('身份证号码不符合规则');
    }
    if (!checkPoneAvailable($('#Phone').val())) {
        isCheck = false;
        $('#PhoneErrorMsg').html('联系电话不符合规则');
    }
    if ($('#Email').val() !== "" &&!checkEmail($('#Email').val())) {
        isCheck = false;
        $('#EmailMsg').html('邮箱地址不符合规则');
    }
    if ($('#EmergencyPhone').val()!==""&&!checkPoneAvailable($('#EmergencyPhone').val())) {
        isCheck = false;
        $('#EmergencyPhoneErrorMsg').html('紧急联系人电话不符合规则');
    }
    return isCheck;
}

function initEditProfileErrorMsg() {
    $('#FullNameErrorMsg').html('');
    $('#IDCardNoErrorMsg').html('');
    $('#PhoneErrorMsg').html('');
    $('#EmailMsg').html('');
    $('#EmergencyPhoneErrorMsg').html('');
}

function getUserProfile() {
    return {
        FullName: $('#FullName').val(), IDCardNo: $('#IDCardNo').val(), Phone: $('#Phone').val(), Email: $('#Email').val(),
        EmergencyContact: $('#EmergencyContact').val(), EmergencyPhone: $('#EmergencyPhone').val(), Education: $('#Education').val(),
        Aptitude: $('#Aptitude').val(), Address: $('#Address').val(), Motto: $('#Motto').val(), UserId: $('#userId').val()};
}

function init() {
    lightyear.loading('show');
    GetUserProfile($('#userId').val());
}

function GetUserProfile(userId) {
    $.ajax({
        type: 'post',
        url: actionUrl.GetUserProfile,
        data: { userId: userId },
        async: false,
        success: function (result) {
            lightyear.loading('hide');
            let User = result;
            if (User) {
                if (User.PhotoPath) {
                    var element = document.getElementById('UserPhoto');
                    element.src = photoPath + User.PhotoPath;
                }
                $('#FullName').val(User.FullName);
                $('#IDCardNo').val(User.IDCardNo);
                $('#Phone').val(User.Phone);
                $('#Email').val(User.Email);
                $('#EmergencyContact').val(User.EmergencyContact);
                $('#EmergencyPhone').val(User.EmergencyPhone);
                $('#Education').val(User.Education);
                $('#Aptitude').val(User.Aptitude);
                $('#Address').val(User.Address);
                $('#Motto').val(User.Motto);
            }
        }
    });
}

function UpLoadPhoto() {
    lightyear.loading('show');
    let file = document.getElementById('uploadImage').files[0];
    if (!file.type.match(/.png|.jpg|.jpeg/)) {
        setTimeout(function () { lightyear.loading('hide'); lightyear.notify('上传错误,文件格式必须为：png/jpg/jpeg', 'warning'); }, 1e3);
        return;
    }
    if (file.size > 20 * 1024 * 1024) {
        setTimeout(function () { lightyear.loading('hide'); lightyear.notify('上传图片文件不能大于2M'); }, 1e3);
        return;
    }
    UpLoad(file);
}

function UpLoad(file) {
    let userId = $('#userId').val();
    let data = new FormData();
    data.append('file', file)
    data.append('userId', userId)
    $.ajax({
        type: 'post',
        url: actionUrl.UploadUserPhoto,
        data: data,
        cache: false,//不需要缓存
        processData: false,
        contentType: false,
        async: false,
        success: function (result) {
            if (result) {
                if (result.IsSuccess) {
                    var element = document.getElementById('UserPhoto');
                    var head = document.getElementById('header-userName-alt');
                    element.src = photoPath + result.Result;
                    head.src = photoPath + result.Result;
                    setTimeout(function () { lightyear.loading('hide'); lightyear.notify('头像修改成功！', 'success'); }, 1e3);
                }
                else {
                    setTimeout(function () { lightyear.loading('hide'); lightyear.notify(result.Result, 'warning'); }, 1e3);
                }
            }
            else {
                setTimeout(function () { lightyear.loading('hide'); lightyear.notify('头像上传失败！', 'warning'); }, 1e3);
            }
        }
    });
}