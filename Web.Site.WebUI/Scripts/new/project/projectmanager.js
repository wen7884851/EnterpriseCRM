function getProjectList(pageindex,projectName) {
    var actionUrl = "";
    var formData = { LoginName: loginName, LoginPwd: loginPwd };
    $.ajax({
        type: 'post',
        url: actionUrl,
        data: formData,
        success: function (result) {
            if (result.IsSuccess) {
                location.href = "@Url.Action("Index", "Dashboard", new { Area = "Common" })";
            } else {
                setTimeout(function () { lightyear.loading('hide'); lightyear.notify(result.Result, 'danger'); }, 1e3);
            }
        }
    });
}