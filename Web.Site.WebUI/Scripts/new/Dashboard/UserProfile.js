var photoPath = "/image/main/userProfile/";
$(function () {
    var element = document.getElementById('UserPhoto');
    element.src = photoPath + "userProfile.jpg";
});

function init() {
    lightyear.loading('show');
    var User = GetUserProfile($('#userId').val());
    if (User) {

    }
}

function GetUserProfile(userId) {
    let actionUrl = "/Core/User/GetUserProfile";
    $.ajax({
        type: 'post',
        url: actionUrl,
        data: { userId: userId },
        async: false,
        success: function (result) {

        })
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
    let actionUrl = "/Core/User/UploadUserPhoto";
    let userId = $('#userId').val();
    let data = new FormData();
    data.append('file', file)
    data.append('userId', userId)
    $.ajax({
        type: 'post',
        url: actionUrl,
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