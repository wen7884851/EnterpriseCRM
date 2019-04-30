var actionUrl = {
    GetProjectRestProportion: '/Project/ProjectManager/GetProjectRestProportion',
    GetProjectById: '/Project/ProjectManager/GetProjectById',
    GetProjectPointListByProjectId: '/Project/ProjectPointManager/GetProjectPointListByProjectId',
    GetPointById: '/Project/ProjectPointManager/GetPointById',
    GetUserStoreListByPointId: '/Project/ProjectUserStore/GetUserStoreListByPointId',
    GetProfessionalType: '/Project/ProjectPointManager/GetProfessionalType',
    CreateProjectPoint: '/Project/ProjectPointManager/CreateProjectPoint',
    GetExportCurrentPointUser: '/Project/ProjectPointManager/GetExportCurrentPointUser',
    CreateUserStore: '/Project/ProjectUserStore/CreateUserStore',
    EditProjectPoint: '/Project/ProjectPointManager/UpdateProjectPoint',
    DeletePoint: '/Project/ProjectPointManager/DeleteProjectPoint',
    GetUserStoreById: '/Project/ProjectUserStore/GetUserStoreById',
    EditUserStore: '/Project/ProjectUserStore/UpdateUserStore',
    DeleteUserStore:'/Project/ProjectUserStore/DeleteUserStore'
};

init();

function init() {
    lightyear.loading('show');
    initProjectPointListInfo(getParam('projectId'));
    lightyear.loading('hide');
}
function initRestProportionInfo(projectId, projectName) {
    $.ajax({
        type: 'post',
        url: actionUrl.GetProjectRestProportion,
        data: { projectId: projectId },
        async: false,
        success: function (result) {
            if (result >= 0) {
                let restProportion = projectName;
                if (result > 0) {
                    restProportion += '-项目分项视图&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;剩余可分配点数：' + result + '%';
                }
                else {
                    restProportion += '-项目分项视图';
                }
                $('#RestProportion').html(restProportion);
            }
        }
    });
}
function initProjectPointListInfo(projectId) {
    initProjectHidenValue(projectId);
    $.ajax({
        type: 'post',
        url: actionUrl.GetProjectPointListByProjectId,
        data: { projectId: projectId },
        async: false,
        success: function (result) {
            if (result) {
                if (result.length > 0) {
                    let html = '';
                    result.forEach(i => {
                        html += '<div class="col-sm-6" id="point_' + i.Id + '">';
                        html += buildPointHtmlValue(i);
                        html += '</div>';
                    });
                    $('#pointList').html(html);
                }
            }
            else {
                setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
            }
        }
    });
}

function CreateProjectPoint() {
    lightyear.loading('show');
    if (checkCreateProjectPoint()) {
       let point = getProjectPointHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.CreateProjectPoint,
            data: point,
            async: false,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        let msg = '分项创建成功！';
                        if (parseFloat(result.Result) > 0) {
                            msg += '项目剩余可分配点数为' + result.Result + '%';
                        }
                        else {
                            msg += '项目已完全分配到分项！';
                        }
                        setTimeout(function () { lightyear.notify(msg, 'success'); }, 1e3);
                        $('#createPointModal').modal('hide');
                        init();
                    }
                    else {
                        setTimeout(function () { lightyear.notify(result.Result, 'warning'); }, 1e3);
                    }
                }
                else {
                    setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
                }
            }
        });
        
    }
    else {
        setTimeout(function () { lightyear.notify('请填写完整信息！', 'warning'); }, 1e3);
    }
    lightyear.loading('hide');
}
function getProjectPointHtmlValue() {
    let selectProfessionalType = $("#ProfessionalType");
    let professionalTypeId = parseInt(selectProfessionalType[0].options[selectProfessionalType[0].selectedIndex].value);
    return {
        ProjectId: getParam('projectId'), ProfessionalTypeId: professionalTypeId, PointName: $('#PointName').val(), PointContent: $('#Content').val(),
        PointProportion: $('#PointProportion').val()};
}
function checkCreateProjectPoint() {
    clearCreatePointErrorMsg();
    let isCheck = true;
    if ($("#PointName").val() === "") {
        $("#PointNameErrorMsg").html('分项名称不为空');
        isCheck = false;
    }
    let selectProfessionalType = $("#ProfessionalType");
    let professionalType = parseInt(selectProfessionalType[0].options[selectProfessionalType[0].selectedIndex].value);
    if (professionalType < 1) {
        $("#ProfessionalTypeErrorMsg").html('请选择专业类别');
        isCheck = false;
    }
    if ($("#PointProportion").val() === "" ) {
        $("#PointProportionErrorMsg").html('分项占比输入有误');
        isCheck = false;
    }
    return isCheck;
}
function OpenCreateProjectPointModal() {
    lightyear.loading('show');
    $.ajax({
        type: 'post', url: actionUrl.GetProfessionalType, async: false, success: function (result) {
            let html = '<option value="0" selected>请选择</option>';
            if (result) {
                for (let index = 0; index < result.length; index++) {
                    html += '<option value="' + result[index].Id + '">' + result[index].TypeName + '</option>';
                }
                $('#ProfessionalType').html(html);
                clearCreatePointErrorMsg();
                initCreatePointHtml();
                $('#createPointModal').modal('show');
            }
            lightyear.loading('hide');
        }
    });
}
function onChangePointProportion(obj,e) {
    obj = setProportionValue(obj);
    let result = obj * $('#ContractMoney').val()/100;
    e.html(result);
    return obj;
}
function clearCreatePointErrorMsg() {
    $("#PointNameErrorMsg").html('');
    $("#ProfessionalTypeErrorMsg").html('');
    $("#PointProportionErrorMsg").html('');
}
function initCreatePointHtml() {
    $("#PointName").val('');
    $("#PointProportion").val('');
    $("#PointContractMoney").html('');
    $("#Content").val('');
}

function buildPointHtmlValue(point) {
    let html = '<div class="card">';
    html += getCardHeader(point);
    html += getPointList(point.Id);
    html += '</div>';
    return html;
}
function getCardHeader(point) {
    let html = '';
    switch (point.Status) {
        case 1:
            html += '<div class="card-header bg-primary">';
            html += '<h4>' + point.PointName + '(项目合同占比' + point.PointProportion + '%)</h4><ul class="card-actions">';
            html += '<li><button type="button" data-toggle="tooltip" title="刷新" onclick="RefreshPoint(' + point.Id + ')"><i class="mdi mdi-autorenew"></i></button></li>'; //Refresh功能
            html += '<li><button type="button" data-toggle="tooltip" title="新增分项成员" onclick="OpenAddUserModal(' + point.Id + ',' + point.PointProportion + ')"><i class="mdi mdi-plus"></i></button></li>'; //AddUser功能
            html += '<li><button type="button" data-toggle="tooltip" title="修改" onclick="OpenEditPointModal(' + point.Id + ')"><i class="mdi mdi-pencil"></i></button></li>'; //Edit功能
            html += '<li><button type="button" data-toggle="tooltip" title="删除" onclick="OpenDeletePointModal(' + point.Id + ')"><i class="mdi mdi-delete-forever"></i></button></li>'; //Delete功能
            html += '</ul></div>';
            break;
        case 2:
            html += '<div class="card-header bg-danger">';
            html += '<h4>' + point.PointName + '(项目合同占比' + point.PointProportion + '%)</h4><ul class="card-actions">';
            html += '<li><button type="button" data-toggle="tooltip" title="刷新" onclick="RefreshPoint(' + point.Id + ')"><i class="mdi mdi-autorenew"></i></button></li>'; //Refresh功能
            html += '<li><button type="button" data-toggle="tooltip" title="新增分项成员" onclick="OpenAddUserModal(' + point.Id + ',' + point.PointProportion + ')"><i class="mdi mdi-plus"></i></button></li>'; //AddUser功能
            html += '<li><button type="button" data-toggle="tooltip" title="修改" onclick="OpenEditPointModal(' + point.Id + ')"><i class="mdi mdi-pencil"></i></button></li>'; //Edit功能
            html += '<li><button type="button" data-toggle="tooltip" title="删除" onclick="OpenDeletePointModal(' + point.Id + ')"><i class="mdi mdi-delete-forever"></i></button></li>'; //Delete功能
            html += '</ul></div>';
            break;
        case 3:
            html += '<div class="card-header bg-success">';
            html += '<h4>' + point.PointName + '(项目合同占比' + point.PointProportion + '%)</h4><ul class="card-actions">';
            html += '<li><button type="button" data-toggle="tooltip" title="刷新" onclick="RefreshPoint(' + point.Id + ')"><i class="mdi mdi-autorenew"></i></button></li>'; //Refresh功能  
            html += '<li><button type="button" data-toggle="tooltip" title="修改" onclick="OpenEditPointModal(' + point.Id + ')"><i class="mdi mdi-pencil"></i></button></li>'; //Edit功能html += '<li><button type="button" data-toggle="tooltip" title="修改" onclick="OpenEditPointModal(' + point.Id + ')"><i class="mdi mdi-pencil"></i></button></li>'; //Edit功能
            html += '<li><button type="button" data-toggle="tooltip" title="删除" onclick="OpenDeletePointModal(' + point.Id + ')"><i class="mdi mdi-delete-forever"></i></button></li>'; //Delete功能
            html += '</ul></div>';
            break;
    }
    return html;
}
function getCardTable(userStore) {
    let html = ' <tr>'; 
    html += ' <td>' + userStore.UserName + '</td>';
    html += ' <td>' + userStore.StoreContent + '</td>';
    html += ' <td>' + userStore.UserProportion + '</td>';
    let btnArray = '<a class="btn btn-xs btn -default" href="#!" title="编辑" data-toggle="tooltip"><i class="mdi mdi-pencil" onclick="OpenEditUserStoreModal(' + userStore.Id + ',' + userStore.ProjectPointId+')"></i></a>';
    btnArray += '<a class="btn btn-xs btn-default" href="#!" title="删除" data-toggle="tooltip"><i class="mdi mdi-window-close" onclick="OpenDeleteEditStoreModal(' + userStore.Id + ',' + userStore.ProjectPointId +')"></i></a>';
    html += ' <td>' + btnArray + '</td><tr>';
    return html;
}
function getPointList(pointId) {
    let html = '<table class="table table-striped"><thead><tr><th>成员名称</th><th>工作内容</th><th>分项工作量占比（%）</th><th>操作</th></tr></thead><tbody>';  //tableHeader
    $.ajax({
        type: 'post',
        url: actionUrl.GetUserStoreListByPointId,
        data: { pointId: pointId },
        async: false,
        success: function (result) {
            if (result) {
                if (result.length > 0) {
                    result.forEach(i => {
                    html += getCardTable(i);
                    });
                }
            }
            else {
                setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
            }
            html += '</tbody></table>';
        }
    });
    return html;
}
function initProjectHidenValue(projectId) {
    $('#ProjectId').val(projectId);
    $.ajax({
        type: 'post',
        url: actionUrl.GetProjectById,
        data: { projectId: projectId },
        async: false,
        success: function (result) {
            if (result) {
                $('#ContractMoney').val(result.ContractMoney);
                initRestProportionInfo(projectId, result.ProjectName);
            }
        }
    });
}


function RefreshPoint(pointId) {
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: actionUrl.GetPointById,
        data: { pointId: pointId},
        async: false,
        success: function (result) {
            if (result) {
                let html = buildPointHtmlValue(result);
                $('#point_' + pointId).html(html);
            }
            else {
                setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}
function OpenAddUserModal(pointId, pointProportion) {
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: actionUrl.GetExportCurrentPointUser,
        data: { pointId: pointId },
        async: false,
        success: function (result) {
            if (result) {
                let userHtml = '<option value="0" selected>请选择</option>';
                if (result.length > 0) {
                result.forEach(i => {
                    userHtml += '<option value="' + i.value + '">' + i.text + '</option>';
                    });
                }
                $("#UserStore").html(userHtml);
                clearCreateUserStoreErrorMsg();
                initCreateUserStoreHtmlValue();
                $('#pointId').val(pointId);
                $('#pointProportion').val(pointProportion);
                $('#createUserStoreModal').modal('show');
            }
            else {
                setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}
function onChangeUserProportion(obj) {
    obj = setProportionValue(obj);
    let pointProportion = parseFloat( $('#pointProportion').val());
    if (obj <= pointProportion) {
        $('#StoreProportionErrorMsg').html('');
        let contractMoney = parseFloat($('#ContractMoney').val());
        let userContractMoney = contractMoney * obj / 100;
        $('#UserContractMoney').html(userContractMoney);
    }
    else {
        $('#StoreProportionErrorMsg').html('成员占比大于分项占比，请重新填写！');
    }
    return obj;
}
function CreateUserStore() {
    lightyear.loading('show');
    if (checkCreateUserStore()) {
        let userStore = getUserStoreHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.CreateUserStore,
            data: userStore,
            async: false,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        let msg = '成员添加完毕！';
                        if (parseFloat(result.Result) > 0) {
                            msg += '分项剩余可分配点数为' + result.Result + '%';
                        }
                        else {
                            msg += '项目分项已完全分配到各成员！';
                        }
                        setTimeout(function () { lightyear.notify(msg, 'success'); }, 1e3);
                        $('#createUserStoreModal').modal('hide');
                        RefreshPoint(userStore.ProjectPointId);
                    }
                    else {
                        setTimeout(function () { lightyear.notify(result.Result, 'warning'); }, 1e3);
                    }
                }
                else {
                    setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
                }
                lightyear.loading('hide');
            }
        });
    }
    else {
        setTimeout(function () { lightyear.notify('请填写完整信息！', 'warning'); }, 1e3);
    }
    
}
function getUserStoreHtmlValue() {
    let userStore = $("#UserStore");
    let userId = parseInt(userStore[0].options[userStore[0].selectedIndex].value);
    return {
        ProjectPointId: $('#pointId').val(), UserId: userId, StoreContent: $('#StoreContent').val()
        , UserProportion: $('#StoreProportion').val()
    };
}
function checkCreateUserStore() {
    clearCreateUserStoreErrorMsg();
    let isCheck = true;
    let pointProportion = parseFloat($('#pointProportion').val());
    let currentProportion = parseFloat($('#StoreProportion').val());
    if (currentProportion > pointProportion) {
        $('#StoreProportionErrorMsg').html('成员占比大于分项占比，请重新填写！');
        isCheck = false;
    }
    let userStore = $("#UserStore");
    let userId = parseInt(userStore[0].options[userStore[0].selectedIndex].value);
    if (userId < 1) {
        $("#UserStoreErrorMsg").html('请选择成员');
        isCheck = false;
    }
    return isCheck;
}
function clearCreateUserStoreErrorMsg() {
    $('#StoreProportionErrorMsg').html('');
    $('#UserStoreErrorMsg').html('');
}

function initCreateUserStoreHtmlValue() {
    $('#StoreProportion').val();
    $('#UserContractMoney').html('');
    $('#StoreContent').val();
}

function EditProjectPoint() {
    lightyear.loading('show');
    if (checkEditProjectPoint()) {
        let point = getProjectEditPointHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.EditProjectPoint,
            data: point,
            async: false,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        let msg = '分项修改成功！';
                        if (parseFloat(result.Result) > 0) {
                            msg += '项目剩余可分配点数为' + result.Result + '%';
                        }
                        else {
                            msg += '项目已完全分配到分项！';
                        }
                        setTimeout(function () { lightyear.notify(msg, 'success'); }, 1e3);
                        $('#editPointModal').modal('hide');
                        init();
                    }
                    else {
                        setTimeout(function () { lightyear.notify(result.Result, 'warning'); }, 1e3);
                    }
                }
                else {
                    setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
                }
                lightyear.loading('hide');
            }
        });
    }
}
function getProjectEditPointHtmlValue() {
    let selectProfessionalType = $("#EProfessionalType");
    let professionalTypeId = parseInt(selectProfessionalType[0].options[selectProfessionalType[0].selectedIndex].value);
    let selectEPointStatus = $("#EPointStatus");
    let ePointStatusId = parseInt(selectEPointStatus[0].options[selectEPointStatus[0].selectedIndex].value);
    return {
        Id: $('#EPoint').val(), ProfessionalTypeId: professionalTypeId, PointName: $('#EPointName').val(), PointContent: $('#EContent').val(),
        PointProportion: $('#EPointProportion').val(), Status: ePointStatusId
    };
}
function checkEditProjectPoint() {
    clearEditPointErrorMsg();
    let isCheck = true;
    if ($("#EPointName").val() === "") {
        $("#EPointNameErrorMsg").html('分项名称不为空');
        isCheck = false;
    }
    let selectProfessionalType = $("#EProfessionalType");
    let professionalType = parseInt(selectProfessionalType[0].options[selectProfessionalType[0].selectedIndex].value);
    if (professionalType < 1) {
        $("#EProfessionalTypeErrorMsg").html('请选择专业类别');
        isCheck = false;
    }
    if ($("#EPointProportion").val() === "") {
        $("#EPointProportionErrorMsg").html('分项占比输入有误');
        isCheck = false;
    }
    return isCheck;
}
function OpenEditPointModal(pointId) {
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: actionUrl.GetPointById,
        data: { pointId: pointId },
        async: false,
        success: function (result) {
            if (result) {
                $('#EPoint').val(pointId);
                clearEditPointErrorMsg();
                SetEditPointHtmlValue(result);
                $('#editPointModal').modal('show');
            }
            else {
                setTimeout(function () { lightyear.notify('获取分项信息失败！', 'warning'); }, 1e3);
            }
        }
    });
    
}
function clearEditPointErrorMsg() {
    $('#EPointNameErrorMsg').html('');
    $('#EProfessionalTypeErrorMsg').html('');
    $('#EPointProportionErrorMsg').html('');
}
function SetEditPointHtmlValue(point) {
    let html = '';
    for (let i = 1; i <= 3; i++) {
        if (point.Status === i) {
            html += '<option value="' + i + '" selected>' + getStatus(i) + '</option>';
        }
        else {
            html += '<option value="' + i + '">' + getStatus(i) + '</option>';
        }
    }
    $('#EPointStatus').html(html);
    $('#EPointName').val(point.PointName);
    $('#EPointProportion').val(point.PointProportion);
    $('#EPointContractMoney').html(point.PointProportion * $('#ContractMoney').val() / 100);
    $('#EContent').val(point.PointContent);
    $.ajax({
        type: 'post', url: actionUrl.GetProfessionalType, async: false, success: function (result) {
            let html = '<option value="0">请选择</option>';
            if (result) {
                for (let index = 0; index < result.length; index++) {
                    if (point.ProfessionalTypeId === result[index].Id) {
                        html += '<option value="' + result[index].Id + '" selected>' + result[index].TypeName + '</option>';
                    }
                    else {
                        html += '<option value="' + result[index].Id + '">' + result[index].TypeName + '</option>';
                    }
                }
            }
            $('#EProfessionalType').html(html);
            lightyear.loading('hide');
        }
    });
}
function getStatus(status) {
    let name = '';
    switch (status) {
        case 1:
            name = '已立项';
            break;
        case 2:
            name = '进行中';
            break;
        case 3:
            name = '已完结';
            break;
        default:
            name = '未知状态';
            break;
    }
    return name;
}
function onchangePointStatus() {
    let status = $("#EPointStatus");
    let statusId = parseInt(status[0].options[status[0].selectedIndex].value);
    if (statusId === 3) {
        $("#EPointName").attr("readOnly", "true");
        $("#EProfessionalType").attr("readOnly", "true");
        $("#EPointProportion").attr("readOnly", "true");
        $("#EContent").attr("readOnly", "true");
    }
    else {
        $("#EPointName").removeAttr("readOnly");
        $("#EProfessionalType").removeAttr("readOnly");
        $("#EPointProportion").removeAttr("readOnly");
        $("#EContent").removeAttr("readOnly");
    }
}

function OpenDeletePointModal(pointId) {
    $('#DPointId').val(pointId);
    $('#DeletePointModal').modal('show');
}
function deletePoint() {
    lightyear.loading('show');
    let pointId = $('#DPointId').val();
    $.ajax({
        type: 'post',
        url: actionUrl.DeletePoint,
        data: { pointId: pointId },
        async: false,
        success: function (result) {
            if (result) {
                if (result.IsSuccess) {
                    init();
                    setTimeout(function () { lightyear.notify('删除分项成功！', 'success'); }, 1e3);
                    $('#DeletePointModal').modal('hide');
                }
                else {
                    setTimeout(function () { lightyear.notify(result.Result, 'warning'); }, 1e3);
                }
            }
            else {
                setTimeout(function () { lightyear.notify('删除分项失败！', 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}

function EditUserStore() {
    lightyear.loading('show');
    if (checkEditUserStore()) {
        let userStore = getEditUserStoreHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.EditUserStore,
            data: userStore,
            async: false,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        let msg = '编辑成功！';
                        if (parseFloat(result.Result) > 0) {
                            msg += '分项剩余可分配点数为' + result.Result + '%';
                        }
                        else {
                            msg += '项目分项已完全分配到各成员！';
                        }
                        setTimeout(function () { lightyear.notify(msg, 'success'); }, 1e3);
                        $('#editUserStoreModal').modal('hide');
                        RefreshPoint(userStore.ProjectPointId);
                    }
                    else {
                        setTimeout(function () { lightyear.notify(result.Result, 'warning'); }, 1e3);
                    }
                }
                else {
                    setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
                }
                lightyear.loading('hide');
            }
        });
    }
    else {
        setTimeout(function () { lightyear.notify('请填写完整信息！', 'warning'); }, 1e3);
        lightyear.loading('hide');
    }
}
function getEditUserStoreHtmlValue() {
    let userStore = $("#EUserStore");
    let userId = parseInt(userStore[0].options[userStore[0].selectedIndex].value);
    return {
        Id: $('#EStore').val(), ProjectPointId: $('#EProjectPointId').val(), UserId: userId, StoreContent: $('#EStoreContent').val()
        , UserProportion: $('#EStoreProportion').val()
    };
}
function checkEditUserStore() {
    clearEditStoreErrorMsg();
    let isCheck = true;
    let pointProportion = parseFloat($('#pointProportion').val());
    let currentProportion = parseFloat($('#EStoreProportion').val());
    if (currentProportion > pointProportion) {
        $('#EStoreProportionErrorMsg').html('成员占比大于分项占比，请重新填写！');
        isCheck = false;
    }
    let userStore = $("#EUserStore");
    let userId = parseInt(userStore[0].options[userStore[0].selectedIndex].value);
    if (userId < 1) {
        $("#EUserStoreErrorMsg").html('请选择成员');
        isCheck = false;
    }
    return isCheck;
}
function OpenEditUserStoreModal(storeId,pointId) {
    lightyear.loading('show');
    clearEditStoreErrorMsg();
    $.ajax({
        type: 'post',
        url: actionUrl.GetUserStoreById,
        data: { storeId: storeId },
        async: false,
        success: function (result) {
            if (result) {
                $('#EStore').val(storeId);
                $('#EProjectPointId').val(pointId);
                SetEditStoreHtmlValue(result);
                $('#editUserStoreModal').modal('show');
            }
            else {
                setTimeout(function () { lightyear.notify('获取分项信息失败！', 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}
function clearEditStoreErrorMsg() {
    $('#EStoreProportionErrorMsg').html('');
    $('#EUserStoreErrorMsg').html('');
}
function SetEditStoreHtmlValue(store) {
    $.ajax({
        type: 'post',
        url: actionUrl.GetExportCurrentPointUser,
        data: { pointId: store.ProjectPointId },
        async: false,
        success: function (result) {
            if (result) {
                let userHtml = '<option value="0">请选择</option>';
                userHtml += '<option value="' + store.UserId + '" selected>' + store.UserName + '</option>';
                if (result.length > 0) {
                    result.forEach(i => {
                        userHtml += '<option value="' + i.value + '">' + i.text + '</option>';
                    });
                }
                $("#EUserStore").html(userHtml);
            }
            else {
                setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
            }
        }
    });
    $("#EStoreProportion").val(store.UserProportion);
    let contractMoney = parseFloat($('#ContractMoney').val());
    let userContractMoney = contractMoney * store.UserProportion / 100;
    $('#EUserContractMoney').html(userContractMoney);
    $("#EStoreContent").val(store.StoreContent);
}

function OpenDeleteEditStoreModal(storeId, DPointId) {
    $('#DStoreId').val(storeId);
    $('#DeleteStoreModal').modal('show');
    $('#DpointId').val(DPointId);
}
function deleteStore() {
    lightyear.loading('show');
    let storeId = $('#DStoreId').val();
    $.ajax({
        type: 'post',
        url: actionUrl.DeleteUserStore,
        data: { storeId: storeId },
        async: false,
        success: function (result) {
            if (result) {
                if (result.IsSuccess) {
                    RefreshPoint($('#DpointId').val());
                    setTimeout(function () { lightyear.notify('删除分项成功！', 'success'); }, 1e3);
                    $('#DeleteStoreModal').modal('hide');
                }
                else {
                    setTimeout(function () { lightyear.notify(result.Result, 'warning'); }, 1e3);
                }
            }
            else {
                setTimeout(function () { lightyear.notify('删除分项失败！', 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}