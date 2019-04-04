var actionUrl = {
    GetProjectContractMoneyById: '/Project/ProjectManager/GetProjectContractMoneyById',
    GetProjectPointListByProjectId: '/Project/ProjectPointManager/GetProjectPointListByProjectId',
    GetUserStoreListByPointId: '/Project/ProjectUserStore/GetUserStoreListByPointId',
    GetProfessionalType: '/Project/ProjectPointManager/GetProfessionalType', CreateProjectPoint: '/Project/ProjectPointManager/CreateProjectPoint'
};

init();

function init() {
    lightyear.loading('show');
    initProjectPointListInfo(getParam('projectId'));
    lightyear.loading('hide');
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
                let html = '';
                result.forEach(i => {
                    html += '<div class="col-sm-6" id="point_'+i.Id+'"><div class="card">';
                    html += initPointHtmlInfo(i);
                    html += '</div></div>';
                });
                $('#pointList').html(html);
            }
            else {
                setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
            }
        }
    });
}
function initPointHtmlInfo(point) {
    let html = getCardHeader(point.Status, point.PointName, point.Id);
    html += getPointList(point.Id);
    return html;
}

function CreateProjectPoint() {
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
    clearCreatePointErrorMsg();
    $.ajax({
        type: 'post', url: actionUrl.GetProfessionalType, async: false, success: function (result) {
            let html = '<option value="0" selected>请选择</option>';
            if (result) {
                for (let index = 0; index < result.length; index++) {
                    html += '<option value="' + result[index].Id + '">' + result[index].TypeName + '</option>';
                }
            }
            $('#ProfessionalType').html(html);
        }
    });
    lightyear.loading('hide');
    $('#createPointModal').modal('show');
}
function onChangePointProportion(obj) {
    obj = setProportionValue(obj);
    let result = obj * $('#ContractMoney').val()/100;
    $('#PointContractMoney').html(result);
    return obj;
}
function clearCreatePointErrorMsg() {
    $("#PointNameErrorMsg").html('');
    $("#ProfessionalTypeErrorMsg").html('');
    $("#PointProportionErrorMsg").html('');
}


function getCardHeader(status,pointName,pointId) {
    let html = '';
    switch (status) {
        case 1:
            html += '<div class="card-header bg-primary">';
            break;
        case 2:
            html += '<div class="card-header bg-danger">';
            break;
        case 3:
            html += '<div class="card-header bg-success">';
            break;
    }
    html += '<h4>' + pointName + '</h4><ul class="card-actions">';
    html += '<li><button type="button" data-toggle="tooltip" title="刷新" onclick="RefreshPoint(' + pointId + ')"><i class="mdi mdi-autorenew"></i></button></li>'; //Refresh功能
    html += '<li><button type="button" data-toggle="tooltip" title="新增分项成员" onclick="OpenAddUserModal(' + pointId + ')"><i class="mdi mdi-plus"></i></button></li>'; //AddUser功能
    html += '<li><button type="button" data-toggle="tooltip" title="修改" onclick="OpenEditPointModal(' + pointId + ')"><i class="mdi mdi-pencil"></i></button></li>'; //Edit功能
    html += '<li><button type="button" data-toggle="tooltip" title="删除" onclick="OpenDeletePointModal(' + pointId + ')"><i class="mdi mdi-delete-forever"></i></button></li>'; //Delete功能
    html += '</ul></div>';
    return html;
}
function getCardTable(point) {
    let html = ' <tr>'; 
    html += ' <td>' + point.UserName + '</td>';
    html += ' <td>' + point.StoreContent + '</td>';
    html += ' <td>' + point.StoreProportion + '</td>';
    let btnArray = '<a class="btn btn-xs btn -default" href="#!" title="编辑" data-toggle="tooltip"><i class="mdi mdi-pencil" onclick="OpenEditUserStoreModal(' + point.Id + ')"></i></a>';
    btnArray += '<a class="btn btn-xs btn-default" href="#!" title="删除" data-toggle="tooltip"><i class="mdi mdi-window-close" onclick="OpenDeleteUserStoreModal(' + point.Id + ')"></i></a>';
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
                forEach(i => {
                    html += getCardTable(i);
                });
            }
            else {
                setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
                html += '</tbody></table>';
            }
        }
    });
    return html;
}
function initProjectHidenValue(projectId) {
    $('#ProjectId').val(projectId);
    $.ajax({
        type: 'post',
        url: actionUrl.GetProjectContractMoneyById,
        data: { projectId: projectId },
        success: function (result) {
            if (result) {
                $('#ContractMoney').val(result);
            }
        }
    });
}