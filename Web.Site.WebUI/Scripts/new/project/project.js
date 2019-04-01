$(document).ready(function () {
    $('.form-control').select2();
});

var query = {};
var actionUrl = { ProjectList: "/Project/projectmanager/ProjectList", GetUserList: "/Common/Account/GetUserList"};
var aoColumns = [
    { "sName": "Id" },
    { "sName": "ProjectName" },
    { "sName": "LeaderName" },
    { "sName": "Content" },
    { "sName": "LinkPerson" },
    { "sName": "LinkPhoneNo" },
    {
        "sName": "Id",
        "fnRender": function (oObj) {
            var btnArray = '<a class="btn btn-xs btn -default" href="#!" title="编辑" onclick="Edit(' + oObj + ') data-toggle="tooltip"><i class="mdi mdi-pencil"></i></a>';
            btnArray += '<a class="btn btn-xs btn-default" href="#!" title="删除"  onclick="Delete(' + oObj + ') data-toggle="tooltip"><i class="mdi mdi-window-close"></i></a>';
            return btnArray;
        }
    }
];

Search(1);

function Search(index) {
    if (index && index <= 0) {
        index = 1;
    }
    query['PageIndex'] = index;
    query['projectName'] = $("#projectName").val() ? $("#projectName").val() : '';
    tablelist.SearchDataTables($("#tablelist tbody"), $("#filter"), actionUrl.ProjectList, aoColumns, query);
}

function CreateProject() {
    if (CheckItem()) {

    }
}

function CheckItem() {

}

function OpenCreateProjectModal(){
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: actionUrl.GetUserList,
        async: false,
        success: function (result) {
            if (result && result.length > 0) {
                let userHtml ='<option value="0">请选择</option>';
                result.forEach(i => {
                    userHtml += '<option value="' + i.value + '">' + i.text + '</option>';
                    $('#createModal').modal('show');
                })
                $("#ProjectLeader").html(userHtml);
            }
            else {
                setTimeout(function () { lightyear.notify('无用户数据，请先配置用户', 'warning'); }, 1e3);
            }
            lightyear.loading('show');
        }
}


