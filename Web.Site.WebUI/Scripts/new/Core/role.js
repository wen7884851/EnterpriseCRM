var query = {};
var actionUrl = { GetRoleList: "/Core/Role/GetRoleList", GetAllModule:"/Core/Module/GetAllModule"};

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
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.ProjectList, aoColumns, query);
}

function OpenCreateRoleModal() {
    $('#CreateRoleModal').modal('show');
    initRoleModuleConfiguration(false);
}

function initRoleModuleConfiguration(IsEdit) {
    let allModule = GetAllModule();
    let html = '<table class="table table-striped"><thead><tr><th><label class="lyear-checkbox checkbox-primary">';
    html += '<input name="checkbox" type="checkbox" id="check-all"><span> 全选</span></label></th></tr></thead><tbody>';
    if (IsEdit) {
        let currentModule = GetRoleCurrentModule();
        allModule.forEach(i => {
            let checked = currentModule.find((e) => (e.Id === i.Id)) ? 'checked' : '';
            switch (i.key) {
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
    }
    else {
        allModule.forEach(i => {
            switch (i.key) {
                case 1:
                    html += '<tr><td><label class="lyear-checkbox checkbox-primary">';
                    html += '<input type="checkbox" class="checkbox-parent" dataid="' + i.DataId +'" value="' + i.Id + '">';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
                case 2:
                    html += '<tr><td class="p-l-20"><label class="lyear-checkbox checkbox-primary">';
                    html += '<input type="checkbox" class="checkbox-parent checkbox-child" dataid="' + i.DataId + '" value="' + i.Id + '">';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
                case 3:
                    html += '<tr><td class="p-l-40"><label class="lyear-checkbox checkbox-primary checkbox-inline">';
                    html += '<input type="checkbox" class="checkbox-child" dataid="' + i.DataId + '" value="' + i.Id + '">';
                    html += '<span> ' + i.Name + '</span></label></td></tr>';
                    break;
            }
        });
    }
    $('.table-responsive').html(html);
}

function GetAllModule() {

}