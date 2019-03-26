var tableData = null;

function InitDataTables(dataTableObj, actionUrl, aoColumns) {
    GetTableList(actionUrl, null);
    BuildDataTable(dataTableObj, aoColumns);
    BuildPageFilter(dataTableObj);
}

function SearchDataTables(dataTableObj, actionUrl, aoColumns, query) {
    GetTableList(actionUrl, query);
    BuildDataTable(dataTableObj, aoColumns);
    BuildPageFilter(dataTableObj);
}

function BuildDataTable(dataTableObj, aoColumns) {

}

function BuildPageFilter(dataTableObj,currentPageIndex) {

}

function GetTableList(actionUrl, query) {
    lightyear.loading('show');
    if (query) {
        $.ajax({
            type: 'post',
            url: actionUrl,
            data: query,
            success: function (result) {
                lightyear.loading('hide');
                if (result) {
                    tableData = result;
                } else {
                    setTimeout(function () { lightyear.notify('数据读取失败，请查看网络！', 'danger'); }, 1e3);
                }
            }
        });
    }
    else {
        $.ajax({
            type: 'post',
            url: actionUrl,
            success: function (result) {
                lightyear.loading('hide');
                if (result) {
                    tableData = result;
                } else {
                    setTimeout(function () { '数据读取失败，请查看网络！', 'danger'); }, 1e3);
                }
            }
        });
    }
    
}