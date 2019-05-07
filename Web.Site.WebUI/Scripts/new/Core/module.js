var query = {};
var actionUrl = {
    GetModuleListByQuery: "/Core/Module/GetModuleListByQuery", GetParentModuleKeyValue: "/Core/Module/GetParentModuleKeyValue",
    GetLayerKeyValue: "/Core/Module/GetLayerKeyValue", CreateModule: "/Core/Module/CreateModule"};

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
    Search();
});

function SetParentModuleHtmlValue() {
    let html = '<option value="0" selected>请选择</option>';
    $.ajax({
        type: 'post',
        url: actionUrl.GetParentModuleKeyValue,
        data: { layer: 1 },
        success: function (result) {
            result.forEach(i => {
                html += '<option value="' + i.value + '">' + i.text + '</option>';
                $('#SParentModule').html(html);
            });
        }
    });
}

function TurnPage(index) {
    if (index && index <= 0) {
        index = 1;
    }
    query['PageIndex'] = index;
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.GetModuleListByQuery, aoColumns, query);
}

function Search() {
    query['PageIndex'] = 1;
    let parentModule = 0;
    let selectParentModule = $("#SParentModule");
    if (selectParentModule[0].selectedIndex && selectParentModule[0].selectedIndex > -1) {
        parentModule = parseInt(selectParentModule[0].options[selectParentModule[0].selectedIndex].value);
    }
    query['ParentModule'] = parentModule;
    query['ModuleName'] = $("#SModuleName").val() ? $("#SModuleName").val() : '';
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.GetModuleListByQuery, aoColumns, query);
}

function OpenCreateModuleModal() {
    GetLayerHtmlValue();
    $('#CreateModuleModal').modal('show');
}
function IsChildModuleChange() {
    let isChildModule = document.getElementById("isChildModule");
    if (isChildModule.checked) {
        document.getElementById("ChildModuleRow").style.visibility = "visible";
    }
    else {
        document.getElementById("ChildModuleRow").style.visibility = "hidden";
    }
}

function LayerChange() {
    let html = '<option value="0" selected>请选择</option>';
    let layer = parseInt($('#Layer')[0].options[$('#Layer')[0].selectedIndex].value);
    if (layer <= 1) {
        $('#ParentModule').html(html);
        return;
    }
    $.ajax({
        type: 'post',
        url: actionUrl.GetParentModuleKeyValue,
        data: { layer: layer-1},
        success: function (result) {
            result.forEach(i => {
                html += '<option value="' + i.value + '">' + i.text + '</option>';
                $('#ParentModule').html(html);
            });
        }
    });
   
    return;
}
function GetLayerHtmlValue() {
    let html = '<option value="0" selected>请选择</option>';
    $('#ParentModule').html(html);
    $.ajax({
        type: 'post',
        url: actionUrl.GetLayerKeyValue,
        success: function (result) {
            result.forEach(i => {
                html += '<option value="' + i.value + '">' + i.text + '</option>';
            });
            $('#Layer').html(html);
        }
    });
}
function CreateModule() {
    if (CheckCreateModule()) {
        lightyear.loading('show');
        let Module = getCreateModuleHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.CreateModule,
            data: Module,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        setTimeout(function () { lightyear.notify('创建成功！', 'success'); }, 1e3);
                        $('#CreateModuleModal').modal('hide');
                        $('#searchBtn').click();
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
function getCreateModuleHtmlValue() {
    let isChildModule = document.getElementById("isChildModule");
    let parentId = parseInt($('#ParentModule')[0].options[$('#ParentModule')[0].selectedIndex].value);
    let layer = parseInt($('#Layer')[0].options[$('#Layer')[0].selectedIndex].value);
    return {
        ParentId: parentId, Name: $("#ModuleName").val(), LinkUrl: isChildModule.checked ? $("#LinkUrl").val() : "", Layer: layer,
        Icon: $("#Icon").val(), OrderSort: $("#OrderSort").val(), Description: $("#Description").val()
    };
}
function CheckCreateModule() {
    initCreateModuleErrorMsg();
    let isCheck = true;
    if ($("#ModuleName").val() === "") {
        $("#ModuleNameErrorMsg").html('模块名称不为空');
        isCheck = false;
    }
    let layer = parseInt($('#Layer')[0].options[$('#Layer')[0].selectedIndex].value);
    if (layer < 1) {
        $("#LayerErrorMsg").html('请选择菜单层级');
        isCheck = false;
    }
    let parentModule = parseInt($('#ParentModule')[0].options[$('#ParentModule')[0].selectedIndex].value);
    if (parentModule < 1 && layer < 1) {
        $("#ParentModuleNameErrorMsg").html('请选择父级菜单');
        isCheck = false;
    }
    let isChildModule = document.getElementById("isChildModule");
    if (isChildModule.checked) {
        if ($("#LinkUrl").val() === "") {
            $("#LinkUrlErrorMsg").html('访问地址不为空');
            isCheck = false;
        }
    }
    return isCheck;
}
function initCreateModuleErrorMsg() {
    $('#ModuleNameErrorMsg').html('');
    $('#LayerErrorMsg').html('');
    $('#ParentModuleNameErrorMsg').html('');
    $('#LinkUrlErrorMsg').html('');
}