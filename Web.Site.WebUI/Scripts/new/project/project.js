﻿var query = {};
var actionUrl = {
    ProjectList: "/Project/projectmanager/ProjectList", GetUserList: "/Common/Account/GetUserList",
    CreateProject: "/Project/projectmanager/CreateProject", GetProjectList: "/Project/projectmanager/ProjectList",
    DeleteProject: "/Project/projectmanager/DeleteProject", SetProjectProportion: "/Project/projectmanager/SetProjectProportion"};
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
            var btnArray = '<a class="btn btn-xs btn -default" href="#!" title="设置系数" data-toggle="tooltip"><i class="mdi mdi-settings"  onclick="OpenSetProportionModal(' + oObj + ')"></i></a>';
            btnArray += '<a class="btn btn-xs btn -default" href="#!" title="编辑" data-toggle="tooltip"><i class="mdi mdi-pencil" onclick="EditProjectModal(' + oObj + ')"></i></a>';
            btnArray += '<a class="btn btn-xs btn-default" href="#!" title="删除" data-toggle="tooltip"><i class="mdi mdi-window-close" onclick="OpenDeleteProjectModal(' + oObj + ')"></i></a>';
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
    if (CheckCreateProjectItem()) {
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
                        closeCreateProjectModal();
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

function closeCreateProjectModal() {
    initCreateProjectErrorMsg();
    initCreateProjectModal();
    $('#projectModal').modal('hide');
}

function GetProjectHtmlValue() {
    return {
        ContractMoney: $("#ContractMoney").val(), TotalCost: $("#TotalCost").val(),
        ProjectName: $("#ProjectName").val(), ProjectLeader: parseInt($("#ProjectLeader")[0].options[$("#ProjectLeader")[0].selectedIndex].value),
        Content: $("#Content").val(), LinkPerson: $("#LinkPerson").val(), LinkPhoneNo: $("#LinkPhoneNo").val(), Address: $("#Address").val()};
}
function CheckCreateProjectItem() {
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


function OpenSetProportionModal(projectId) {
    lightyear.loading('show');
    initSetProportionErrorMsg();
    $.ajax({
        type: 'post',
        url: actionUrl.GetProjectList,
        data: { projectId: projectId },
        async: false,
        success: function (result) {
            if (result && result.Items.length > 0) {
                let project = result.Items[0];
                $('#SProjectId').val(projectId);
                initSetProportionHtmlValue(project);
                $('#setProportionModal').modal('show');
            }
            else {
                setTimeout(function () { lightyear.notify('无项目数据，请查证！', 'warning'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });
}
function setProjectProportion(){
    if (checkSetProjectProportion()) {
        lightyear.loading('show');
        let projectProportion = GetProjectProportionHtmlValue();
        $.ajax({
            type: 'post',
            url: actionUrl.SetProjectProportion,
            data: projectProportion,
            async: false,
            success: function (result) {
                if (result) {
                    if (result.IsSuccess) {
                        setTimeout(function () { lightyear.notify('设置成功', 'success'); }, 1e3);
                        $('#setProportionModal').modal('hide');
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
function checkSetProjectProportion() {
    initSetProportionErrorMsg();
    let isCheck = true;
    let selectManagementer = $("#Managementer");
    let projectManagementer = parseInt(selectManagementer[0].options[selectManagementer[0].selectedIndex].value);
    if (parseFloat($('#ManagementProportion').val()) > 0 && projectManagementer < 1) {
        $('#ManagementerErrorMsg').html('请选择管理负责人');
        isCheck = false;
    }
    let selectAuditer = $("#Auditer");
    let projectAuditer = parseInt(selectAuditer[0].options[selectAuditer[0].selectedIndex].value);
    if (parseFloat($('#AuditProportion').val()) > 0 && projectAuditer < 1) {
        $('#AuditerErrorMsg').html('请选择审核负责人');
        isCheck = false;
    }
    let selectJudgementer = $("#Judgementer");
    let projectJudgementer = parseInt(selectJudgementer[0].options[selectJudgementer[0].selectedIndex].value);
    if (parseFloat($('#JudgementProportion').val()) > 0 && projectJudgementer < 1) {
        $('#JudgementerErrorMsg').html('请选择项目管理人员');
        isCheck = false;
    }
    return isCheck;
}
function GetProjectProportionHtmlValue() {
    let selectManagementer = $("#Managementer");
    let projectManagementer = parseInt(selectManagementer[0].options[selectManagementer[0].selectedIndex].value);
    let selectAuditer = $("#Auditer");
    let projectAuditer = parseInt(selectAuditer[0].options[selectAuditer[0].selectedIndex].value);
    let selectJudgementer = $("#Judgementer");
    let projectJudgementer = parseInt(selectJudgementer[0].options[selectJudgementer[0].selectedIndex].value);
    return {
        Id: $('#SProjectId').val(), CommissionProportion: $('#CommissionProportion').val(),
        ManagementProportion: $('#ManagementProportion').val(), Managementer: projectManagementer,
        AuditProportion: $('#AuditProportion').val(), Auditer: projectAuditer,
        JudgementProportion: $('#JudgementProportion').val(), Judgementer: projectJudgementer};
}
function changePerson(obj, changeValue) {
    var index = obj.selectedIndex; 
    var value = parseInt(obj.options[index].value);
    if (value === 0) {
        changeValue.val('0');
    }
}
function changeProportionFund(obj,changeFund) {
    let value = clearNoNum(obj);
    if (value > 100) {
        let o = value.toString();
        value = parseFloat(o.substr(0, o.length-1));
    }
    if (value <= 0) {
        value = 0;
    }
    let ContractMoney = $("#StateContractMoney").val();
    changeFund.html(ContractMoney * value / 100);
    return value;
}
function initSetProportionHtmlValue(project) {
    setProjectCommission(project.ContractMoney, project.CommissionProportion ? project.CommissionProportion : 0);
    setProjectManagement(project.ContractMoney, project.ManagementProportion ? project.ManagementProportion : 0);
    setProjectAudit(project.ContractMoney, project.AuditProportion ? project.AuditProportion : 0);
    setProjectJudgement(project.ContractMoney, project.JudgementProportion ? project.JudgementProportion : 0);
    initSetProportionUser(project.Managementer, project.Auditer, project.Judgementer);
}
function setProjectCommission(ContractMoney, CommissionProportion) {
    CommissionFund = ContractMoney * CommissionProportion / 100;
    $('#StateContractMoney').val(ContractMoney);
    $('#CommissionProportion').val(CommissionProportion);
    $('#CommissionFund').html(CommissionFund);
}
function setProjectManagement(ContractMoney, ManagementProportion) {
    ManagementFund = ContractMoney * ManagementProportion / 100;
    $('#ManagementProportion').val(ManagementProportion);
    $('#ManagementFund').html(ManagementFund);
}
function setProjectAudit(ContractMoney, AuditProportion) {
    AuditFund = ContractMoney * AuditProportion / 100;
    $('#AuditProportion').val(AuditProportion);
    $('#AuditFund').html(AuditFund);
}
function setProjectJudgement(ContractMoney, JudgementProportion) {
    JudgementFund = ContractMoney * JudgementProportion / 100;
    $('#AuditProportion').val(JudgementProportion);
    $('#AuditFund').html(JudgementFund);
}
function initSetProportionUser(Managementer, Auditer, Judgementer) {
    $.ajax({
        type: 'post',
        url: actionUrl.GetUserList,
        async: false,
        success: function (result) {
            if (result && result.length > 0) {
                let userManagementHtml = (Managementer > 0) ? '<option value="0">请选择</option>' : '<option value="0" selected>请选择</option>';
                let userAuditHtml = (Auditer > 0) ? '<option value="0">请选择</option>' : '<option value="0" selected>请选择</option>';
                let userJudgementHtml = (Judgementer > 0) ? '<option value="0">请选择</option>' : '<option value="0" selected>请选择</option>';
                result.forEach(i => {
                    if (i.value === Managementer) {
                        userManagementHtml += '<option value="' + i.value + ' selected">' + i.text + '</option>';
                    }
                    else {
                        userManagementHtml += '<option value="' + i.value + ' ">' + i.text + '</option>';
                    }
                    if (i.value === Auditer) {
                        userAuditHtml += '<option value="' + i.value + ' selected">' + i.text + '</option>';
                    }
                    else {
                        userAuditHtml += '<option value="' + i.value + ' ">' + i.text + '</option>';
                    }
                    if (i.value === Judgementer) {
                        userJudgementHtml += '<option value="' + i.value + ' selected">' + i.text + '</option>';
                    }
                    else {
                        userJudgementHtml += '<option value="' + i.value + ' ">' + i.text + '</option>';
                    }
                });
                $("#Managementer").html(userManagementHtml);
                $("#Auditer").html(userAuditHtml);
                $("#Judgementer").html(userJudgementHtml);
            }
            else {
                setTimeout(function () { lightyear.notify('无用户数据，请先配置用户', 'warning'); }, 1e3);
            }
        }
    });
}
function initSetProportionErrorMsg() {
    $("#ManagementerErrorMsg").html('');
    $("#AuditerErrorMsg").html('');
    $("#JudgementerErrorMsg").html('');
}

function OpenDeleteProjectModal(projectId) {
    $('#DProjectId').val(projectId);
    $('#DeleteProjectModal').modal('show');
}
function deleteProject() {
    lightyear.loading('show');
    let projectId = $('#DProjectId').val();
    $.ajax({
        type: 'post',
        url: actionUrl.DeleteProject,
        data: { projectId: projectId },
        async: false,
        success: function (result) {
            if (result) {
                setTimeout(function () { lightyear.notify('删除项目成功！', 'success'); }, 1e3);
                $('#searchBtn').click();
            }
            else {
                setTimeout(function () { lightyear.notify('删除失败', 'warning'); }, 1e3);
            }
            $('#DeleteProjectModal').modal('hide');
            lightyear.loading('hide');
        }
    });
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