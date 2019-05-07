var tablelist = function () {
    return {
        InitDataTables: function (dataTableObj, pageFilterObj, actionUrl, aoColumns) {
            let tableData;
            tableData=GetTableList(actionUrl, null);
            BuildDataTable(dataTableObj, aoColumns, tableData);
            BuildPageFilter(pageFilterObj, 1, tableData.TotalItemsCount);
        },
        SearchDataTables: function (dataTableObj, pageFilterObj, actionUrl, aoColumns, query) {
            let tableData;
            tableData=GetTableList(actionUrl, query);
            BuildDataTable(dataTableObj, aoColumns, tableData);
            BuildPageFilter(pageFilterObj, query.PageIndex, tableData.TotalItemsCount);
        }
    };

    function BuildDataTable(dataTableObj, aoColumns, tableData) {
        let table ='';
        tableData.Items.forEach(i => {
            table += '<tr>';
            aoColumns.forEach(c => {
                table += BuildCellData(i, c);
            });
            table += '</tr>';
        });
        dataTableObj.html(table);
    }

    function BuildCellData(item, col) {
        let content = col.fnRender ? col.fnRender(item ? item : null) : item[col.sName];
        if (col.fnRender) {
            return '<td>' + content + '</td>';
        }
        let value = item[col.sName] ? item[col.sName] : "";
        return '<td>' + value + '</td>';
    }

    function BuildPageFilter(pageFilterObj, currentPageIndex, totalItemsCount) {
        let filter='';
        let pageCount = Math.ceil(totalItemsCount / 10);
        if (pageCount <= 3) {
            for (var i = 1; i <= pageCount; i++) {
                if (i === currentPageIndex) {
                    filter += '<li class="active"><span>第' + i + '页</span></li>';
                }
                else {
                    filter += '<li><a href="#" onclick=TurnPage(' + i + ')>第' + i + '页</a></li>'
                }
            }
        }
        else {
            if (currentPageIndex > 2) {
                filter += '<li ><a href="#" onclick=TurnPage(1)>第1页</a></li>';
            }
            filter += '<li ><a href="#" onclick=TurnPage(' + currentPageIndex - 1 + ')>上1页</a></li>';
            filter += '<li class="active"><span>第' + currentPageIndex + '页</span></li>';
            filter += '<li ><a href="#" onclick=TurnPage(' + currentPageIndex + 1 + ')>下1页</a></li>';
            if (pageCount - currentPageIndex > 1) {
                filter += '<li ><a href="#" onclick=TurnPage(' + pageCount + ')>第' + pageCount + '页</a></li>';
            }
        }
        pageFilterObj.html(filter);
    }


    function GetTableList(actionUrl, query) {
        let tableData = "";
        lightyear.loading('show');
        if (query) {
            $.ajax({
                type: 'post',
                url: actionUrl,
                data: query,
                async: false,
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
                async: false,
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
        return tableData;
    }
}();