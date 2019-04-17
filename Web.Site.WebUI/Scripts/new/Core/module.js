var query = {};
var actionUrl = { GetModuleListByQuery: "/Core/Module/GetModuleListByQuery", GetParentModuleKeyValue: "/Core/Module/GetParentModuleKeyValue"};

var aoColumns = [
    {
        "sName": "Name",
        "fnRender": function (oObj) {
            return '<a href="#" onclick="OpenEditModuleModal(' + oObj.Id + ')">' + oObj.Name + '</a>';
        }
    },
    {
        "sName": "Layer", "fnRender": function (oObj) {
            return oObj.Layer+'  级菜单';
        }
    },
    { "sName": "LinkUrl" },
    { "sName": "Icon" },
    { "sName": "OrderSort" },
    {
        "sName": "Enabled",
        "fnRender": function (oObj) {
            let html = '<label class="lyear-switch switch-solid switch-primary">';
            if (oObj.Enabled) {
                html += '<input type="checkbox" checked="" onchange="enabledChange(' + oObj.Id + ')">';
            }
            else {
                html += '<input type="checkbox" onchange="enabledChange(' + oObj.Id + ')">';
            }
            html += '<span></span></label>';
            return html;
        }
    },
    { "sName": "RoleName" },
    {
        "sName": "Id",
        "fnRender": function (oObj) {
            let btnArray = '<a class="btn btn-xs btn-default" href="#!" title="删除" data-toggle="tooltip"><i class="mdi mdi-delete" onclick="OpenDeleteModuleModal(' + oObj.Id + ')"></i></a>';
            return btnArray;
        }
    }
];

$(function () {
    SetParentModuleHtmlValue();
    Search(1);
});

function SetParentModuleHtmlValue() {
    $.ajax({
        type: 'post',
        url: actionUrl.GetParentModuleKeyValue,
        success: function (result) {
            let html = '<option value="0" selected>请选择</option>';
            result.forEach(i => {
                html += '<option value="' + i.value + '">' + i.text + '</option>';
            });
            $('#SParentModule').html(html);
        }
    });
}

function Search(index) {
    if (index && index <= 0) {
        index = 1;
    }
    query['PageIndex'] = index;
    let selectParentModule = $("#SParentModule");
    let projectLeader = parseInt(selectParentModule[0].options[selectParentModule[0].selectedIndex].value);
    if (projectLeader < 1) {
        projectLeader = 0;
    }
    query['ParentModule'] = projectLeader;
    query['ModuleName'] = $("#ModuleName").val() ? $("#ModuleName").val() : '';
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.GetModuleListByQuery, aoColumns, query);
}

function OpenCreateModuleModal() {
    $('#CreateModuleModal').modal('show');
}