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
    {
        "sName": "LastLoginTime",
        "fnRender": function (oObj) {
            let date = new Date(parseInt(oObj.LastLoginTime.replace("/Date(", "").replace(")/", ""), 10));
            let time_str = date.toLocaleDateString()+"  " + date.toLocaleTimeString();
            return time_str;
        }
    },
    { "sName": "RoleName" },
    {
        "sName": "Id",
        "fnRender": function (oObj) {
            let btnArray = '<a class="btn btn-xs btn-default" href="#!" title="重置密码" data-toggle="tooltip" style="margin-left:20px"><i class="mdi mdi-lock-reset" onclick="OpenDeleteUserModal(' + oObj.Id + ')"></i></a>';
            btnArray += '<a class="btn btn-xs btn-default" href="#!" title="删除" data-toggle="tooltip"><i class="mdi mdi-delete" onclick="OpenDeleteUserModal(' + oObj.Id + ')"></i></a>';
            return btnArray;
        }
    }
];

$(function () {
    Search(1);
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

function OpenCreateUserModal() {

}
