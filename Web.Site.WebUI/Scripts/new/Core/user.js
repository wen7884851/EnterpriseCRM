var query = {};
var actionUrl = { GetUserListByQuery: "/Core/User/GetUserListByQuery" };

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
    { "sName": "LastLoginTime" },
    { "sName": "RoleName" },
    {
        "sName": "Id",
        "fnRender": function (oObj) {
            let btnArray = '<a class="btn btn-xs btn-default" href="#!" title="重置密码" data-toggle="tooltip"><i class="mdi mdi-window-close" onclick="OpenDeleteUserModal(' + oObj.Id + ')"></i></a>';
            btnArray += '<a class="btn btn-xs btn-default" href="#!" title="删除" data-toggle="tooltip"><i class="mdi mdi-window-close" onclick="OpenDeleteUserModal(' + oObj.Id + ')"></i></a>';
            return btnArray;
        }
    }
];

$(function () {
    Search();
});

function Search(index) {
    if (index && index <= 0) {
        index = 1;
    }
    query['PageIndex'] = index;
    query['LoginName'] = $("#LoginName").val() ? $("#LoginName").val() : '';
    query['FullName'] = $("#FullName").val() ? $("#FullName").val() : '';
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.GetUserListByQuery, aoColumns, query);
}

