var query = {};
var actionUrl = {
    GetRoleList: "/Core/Role/GetRoleList", GetAllModule: "/Core/Module/GetAllModule", CreateRole: "/Core/Role/CreateRole",
    GetRoleModuleByRoleId: "/Core/Role/GetRoleModuleByRoleId"};

var aoColumns = [
    {
        "sName": "Name",
        "fnRender": function (oObj) {
            return '<a href="#" onclick="OpenEditRoleModal(' + oObj.RoleId + ')">' + oObj.Name + '</a>';
        }
    },
    { "sName": "Description" },
    {
        "sName": "UserCount", "fnRender": function (oObj) {
            return '<a href="#" onclick="OpenRoleUserListModal(' + oObj.RoleId + ')">' + oObj.UserCount + '</a>';
        }
    },
    {
        "sName": "Enabled",
        "fnRender": function (oObj) {
            let html = '<label class="lyear-switch switch-solid switch-primary">';
            if (oObj.Enabled) {
                html += '<input type="checkbox" checked="" onchange="enabledChange(' + oObj.RoleId + ',0)">';
            }
            else {
                html += '<input type="checkbox" onchange="enabledChange(' + oObj.RoleId + ',1)">';
            }
            html += '<span></span></label>';
            return html;
        }
    },
    {
        "sName": "RoleId",
        "fnRender": function (oObj) {
            let btnArray = '<a class="btn btn-xs btn-default" href="#!" title="删除" data-toggle="tooltip"><i class="mdi mdi-delete" onclick="OpenDeleteRoleModal(' + oObj.RoleId + ')"></i></a>';
            return btnArray;
        }
    }
];

$(function () { Search(); });

function TurnPage(index) {
    if (index && index <= 0) {
        index = 1;
    }
    query['PageIndex'] = index;
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.ProjectList, aoColumns, query);
}

function Search() {
    query['PageIndex'] = 1;
    query['Name'] = $("#SRoleName").val() ? $("#SRoleName").val() : '';
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.GetRoleList, aoColumns, query);
}

function ClearCreateHtml() {
    $('#RoleName').val('');
    $('#Description').val('');
    $('#RoleNameErrorMsg').html('');
    $('#EditRole').hide();
    $('#CreateRole').show();
}
function OpenCreateRoleModal() {
    ClearCreateHtml();
    $('#CreateRoleModal').modal('show');
    $('#initCreateli').click();
    initRoleModuleConfiguration(false);
}
function CreateRole() {
    let role = GetRoleHtmlValue();
    if (CheckCreateRole(role)) {
        lightyear.loading('show');
        $.ajax({
            type: 'post',
            url: actionUrl.CreateRole,
            data: role,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        setTimeout(function () { lightyear.notify('创建成功！', 'success'); }, 1e3);
                        $('#CreateRoleModal').modal('hide');
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
        if (role.Name === '') {
            setTimeout(function () { lightyear.notify('角色名为空', 'warning'); }, 1e3);
        }
        lightyear.loading('hide');
    }
}
function CheckCreateRole(role) {
    $('#RoleNameErrorMsg').html('');
    let isChecked = true;
    if (role.Name === '') {
        $('#RoleNameErrorMsg').html('角色名不能为空');
        isChecked = false;
    }
    if (role.RoleModuleConfiguration.length < 1) {
        setTimeout(function () { lightyear.notify('该角色没有选择一个菜单', 'warning'); }, 1e3);
    }
    return isChecked;
}
function GetRoleHtmlValue() {
    return {
        Name: $('#RoleName').val(), RoleModuleConfiguration: GetRoleModuleConfiguration(), Description: $('#Description').val()
    };
}

function OpenEditRoleModal(roleId) {
    ClearEditHtml();
    $('#roleId').val(roleId);
    initRoleModuleConfiguration(true);
    $('#EditRoleModal').modal('show');
    $('#initEditli').click();
}
function ClearEditHtml() {
    $('#ERoleName').val('');
    $('#EDescription').val('');
    $('#ERoleNameErrorMsg').html('');
}

function GetRoleModuleConfiguration() {
    let id = [];
    $("#CreateRoleModuleConfiguration input[type='checkbox']").each(function () {
        if ($(this).is(':checked')) {
            id.push($(this).val());
        }
    });
    return id;
}
function initRoleModuleConfiguration(IsEdit) {
    let allModule = GetAllModule();
    let html = '';
    let e;
    if (IsEdit) {
        html += '<table class="table table-striped" id="EditRoleModuleConfiguration"><thead><tr><th><label class="lyear-checkbox checkbox-primary">';
        html += '<input name="checkbox" type="checkbox" id="check-all"><span> 全选</span></label></th></tr></thead><tbody>';
        let currentModule = GetRoleCurrentModule();
        allModule.forEach(i => {
            let checked = currentModule.find((e) => (e.Id === i.Id)) ? 'checked' : '';
            switch (i.Layer) {
                case 1:
                    html += '<tr><td><label class="lyear-checkbox checkbox-primary">';
                    html += '<input type="checkbox" class="checkbox-parent" dataid="' + i.DataId + '" value="' + i.Id + '" ' + checked +'>';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
                case 2:
                    html += '<tr><td class="p-l-20"><label class="lyear-checkbox checkbox-primary">';
                    html += '<input type="checkbox" class="checkbox-parent checkbox-child" dataid="' + i.DataId + '" value="' + i.Id + '" ' + checked +'>';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
                case 3:
                    html += '<tr><td class="p-l-40"><label class="lyear-checkbox checkbox-primary checkbox-inline">';
                    html += '<input type="checkbox" class="checkbox-child" dataid="' + i.DataId + '" value="' + i.Id + '" ' + checked +'>';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
            }
        });
        $('#Edit_RoleModuleConfiguration').html(html);
        e = $('#Edit_RoleModuleConfiguration');
    }
    else {
        html += '<table class="table table-striped" id="CreateRoleModuleConfiguration"><thead><tr><th><label class="lyear-checkbox checkbox-primary">';
        html += '<input name="checkbox" type="checkbox" id="check-all"><span> 全选</span></label></th></tr></thead><tbody>';
        allModule.forEach(i => {
            switch (i.Layer) {
                case 1:
                    html += '<tr><td><label class="lyear-checkbox checkbox-primary">';
                    html += '<input name="rules[]" type="checkbox" class="checkbox-parent" dataid="' + i.DataId +'" value="' + i.Id + '">';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
                case 2:
                    html += '<tr><td class="p-l-20"><label class="lyear-checkbox checkbox-primary">';
                    html += '<input name="rules[]" type="checkbox" class="checkbox-parent checkbox-child" dataid="' + i.DataId + '" value="' + i.Id + '">';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
                case 3:
                    html += '<tr><td class="p-l-40"><label class="lyear-checkbox checkbox-primary checkbox-inline">';
                    html += '<input name="rules[]" type="checkbox" class="checkbox-child" dataid="' + i.DataId + '" value="' + i.Id + '">';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
            }
        });
        $('#Create_RoleModuleConfiguration').html(html);
        e = $('#Create_RoleModuleConfiguration');
    }
    e.find("#check-all").change(function () {
        $("input[type='checkbox']").prop('checked', $(this).prop("checked"));
    });
    checkRegistered(allModule,e);
}
function GetRoleCurrentModule() {
    let roleId = $('#roleId').val();
    let Id = [];
    $.ajax({
        type: 'post',
        url: actionUrl.GetRoleModuleByRoleId,
        data: {
            roleId: roleId
        },
        async: false,
        success: function (result) {
            Id = result;
        }
    });
    return Id;
}

function checkRegistered(obj,e) {
    obj.forEach(i => {
        e.find("input[dataid='" + i.DataId + "']").change(function () {
            e.find("input[dataid*='" + i.DataId + "']").prop('checked', $(this).prop("checked"));
            let parentIds = i.DataId.split('-');
            parentIds.splice(0, 1);
            parentIds.splice((parentIds.length - 1), 1);
            if (parentIds.length > 0) {
                parentIds.forEach(j => {
                    e.find("input[value='" + j + "']").prop('checked', $(this).prop("checked"));
                });
            }
        });
    });
}

function GetAllModule() {
    let moduleList;
    $.ajax({
        type: 'post',
        url: actionUrl.GetAllModule,
        async: false,
        success: function (result) {
            moduleList = result;
        }
    });
    return moduleList;
}

