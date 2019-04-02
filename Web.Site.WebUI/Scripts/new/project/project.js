var query = {};
var actionUrl = {
    ProjectList: "/Project/projectmanager/ProjectList", GetUserList: "/Common/Account/GetUserList",
    CreateProject: "/Project/projectmanager/CreateProject"};
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
            var btnArray = '<a class="btn btn-xs btn -default" href="#!" title="编辑" onclick="SetProportionModal(' + oObj + ') data-toggle="tooltip"><i class="mdi mdi-pencil"></i></a>';
            btnArray += '<a class="btn btn-xs btn -default" href="#!" title="编辑" onclick="EditProjectModal(' + oObj + ') data-toggle="tooltip"><i class="mdi mdi-pencil"></i></a>';
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
        initCreateProjectErrorMsg();
        lightyear.loading('show');
        let project = GetProjectHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.CreateProject,
            data: project,
            async: false,
            success: function (result) {
                if (result ) {
                    if (result.IsSuccess) {
                        setTimeout(function () { lightyear.notify('创建成功,请设置提成系数，默认可分配系数为25%', 'success'); }, 1e3);
                        closeModal();
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
    else {
        setTimeout(function () { lightyear.notify('请填写完整信息！', 'warning'); }, 1e3);
    }
}

function OpenCreateProjectModal() {
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: actionUrl.GetUserList,
        async: false,
        success: function (result) {
            if (result && result.length > 0) {
                let userHtml = '<option value="0" selected>请选择</option>';
                result.forEach(i => {
                    userHtml += '<option value="' + i.value + '">' + i.text + '</option>';
                    $('#projectModal').modal('show');
                });
                $("#ProjectLeader").html(userHtml);
                
            }
            else {
                setTimeout(function () { lightyear.notify('无用户数据，请先配置用户', 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}

function closeModal() {
    initCreateProjectErrorMsg();
    initCreateProjectModal();
    $('#projectModal').modal('hide');
}

function GetProjectHtmlValue() {
    return {
        ProjectName: $("#ProjectName").val(), ProjectLeader: parseInt($("#ProjectLeader")[0].options[$("#ProjectLeader")[0].selectedIndex].value),
        Content: $("#Content").val(), LinkPerson: $("#LinkPerson").val(), LinkPhoneNo: $("#LinkPhoneNo").val(), Address: $("#Address").val()};
}

function CheckItem() {
    initCreateProjectErrorMsg();
    let isCheck = true;
    let projectName = $("#ProjectName").val();
    if (projectName === "") {
        $("#ProjectNameErrorMsg").html('项目名称不为空');
        isCheck = false;
    }
    let selectLeader = $("#ProjectLeader");
    let projectLeader = parseInt(selectLeader[0].options[selectLeader[0].selectedIndex].value);
    if (projectLeader < 1) {
        $("#ProjectLeaderErrorMsg").html('请选择项目负责人');
        isCheck = false;
    }
    let totalCost = parseFloat($("#TotalCost").val());
    if (isNaN(totalCost) || totalCost <= 0) {
        $("#TotalCostErrorMsg").html('总造价输入有误');
        isCheck = false;
    }
    let contractMoney = parseFloat($("#ContractMoney").val());
    if (isNaN(contractMoney) || contractMoney <= 0) {
        $("#ContractMoneyErrorMsg").html('合同金额输入有误');
        isCheck = false;
    }
    return isCheck;
}

function initCreateProjectErrorMsg() {
    $("#ProjectNameErrorMsg").html('');
    $("#ProjectLeaderErrorMsg").html('');
    $("#TotalCostErrorMsg").html('');
    $("#ContractMoneyErrorMsg").html('');
}

function initCreateProjectModal() {
    $("#ProjectName").val('');
    $("#LinkPerson").val('');
    $("#LinkPhoneNo").val('');
    $("#TotalCost").val('');
    $("#ContractMoney").val('');
    $("#Address").val('');
    $("#Content").val('');
}

function clearNoNum(obj) {
    obj = obj.replace(/[^\d.]/g, "");  //清除“数字”和“.”以外的字符  
    obj = obj.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的  
    obj = obj.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    if (obj.indexOf(".") < 0 && obj !== "") {//以上已经过滤，此处控制的是如果没有小数点，首位不能为类似于 01、02的金额 
        obj = parseFloat(obj);
    }
    return obj;
} 