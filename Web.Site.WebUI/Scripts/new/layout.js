$(function () {
    let roleId = 1;
    lightyear.loading('show');
    $.ajax({
        type: 'post',
        url: "/Core/Role/GetMenuByRoleId",
        data: { roleId: roleId },
        success: function (result) {
            if (result && result.length > 0) {
                BuildMenu(result);
                $(".nav-item-has-subnav>a").click(function () {
                    $(this).parent().siblings().find(".nav-subnav").slideUp();
                    $(this).parent().find(".nav-subnav").slideToggle();
                });
            }
            else {
                setTimeout(function () { lightyear.notify('获取菜单失败', 'danger'); }, 1e3);
            }
            lightyear.loading('hide');
        }
    });

});

function BuildMenu(menuList) {
    menuList.sort(sortLayer);
    let sameLayerModule = [];
    let layer = menuList[0].Layer;
    for (let i = 0; i < menuList.length; i++) {
        if (layer === menuList[i].Layer) {
            sameLayerModule.push(menuList[i]);
        }
        else {
            BuildMenuForSameLayer(sameLayerModule);
            layer = menuList[i].Layer;
            sameLayerModule = [];
            sameLayerModule.push(menuList[i]);
        }
    }
    BuildMenuForSameLayer(sameLayerModule);
}

function BuildMenuLayer0(menuList) {
    let html = '<li class="nav-item"> <a href="/Common/Dashboard/Index"><i class="mdi mdi-home"></i> 首页</a> </li>';
    menuList.sort(sortOrderSort);
    for (let j = 0; j < menuList.length; j++) {
        if (menuList[j].LinkUrl === null || menuList[j].LinkUrl === "") {
            html += BuildParentMenu(menuList[j]);
        }
        else {
            html += BuildMenuChild(menuList[j]);
        }
    }
    $('#home_menu_list').html(html);
}

function BuildMenuForSameLayer(menuList) {
    if (menuList[0].Layer === 1) {
        BuildMenuLayer0(menuList);
    }
    else {
        menuList.sort(sortParentId);
        let parentId = menuList[0].ParentId;
        let sameLayerParentModule = [];
        for (let j = 0; j < menuList.length; j++) {
            if (parentId === menuList[j].ParentId) {
                sameLayerParentModule.push(menuList[j]);
            }
            else {
                BuildMenuForSameLayerParent(sameLayerParentModule);
                sameLayerParentModule = [];
                parentId = menuList[j].ParentId;
                sameLayerParentModule.push(menuList[j]);
            }
        }
        BuildMenuForSameLayerParent(sameLayerParentModule);
    }
}

function BuildMenuForSameLayerParent(menuSameLayerParentList) {
    let parentId = menuSameLayerParentList[0].ParentId;
    let html = "";
    menuSameLayerParentList.sort(sortOrderSort);
    for (let k = 0; k < menuSameLayerParentList.length; k++) {
        if (menuSameLayerParentList[k].LinkUrl === null || menuSameLayerParentList[k].LinkUrl === "") {
            html += BuildParentMenu(menuSameLayerParentList[k]);
        }
        else {
            html += BuildMenuChild(menuSameLayerParentList[k]);
        }
    }
    var obj = document.getElementsByName('menu_' + parentId);
    obj[0].innerHTML = html;
}

function BuildParentMenu(menu) {
    let html = '<li class="nav-item nav-item-has-subnav">'
    let icon = menu.Icon !== "" ? '<i class="' + menu.Icon + '"></i>' : '';
    html += '<a href="javascript:void(0)">' + icon + menu.Name + '</a>';
    html += '<ul class="nav nav-subnav" name="menu_' + menu.Id + '">';
    html += '</ul></li>';
    return html;
}

function BuildMenuChild(menu) {
    let icon = menu.Icon !== "" ? '<i class="' + menu.Icon + '"></i>' : '';
    return '<li><a href="' + menu.LinkUrl + '">' + icon + menu.Name + '</a></li>';
}

function sortParentId(a, b) {
    return a.ParentId - b.ParentId
}

function sortOrderSort(a, b) {
    return a.OrderSort - b.OrderSort
}

function sortLayer(a, b) {
    return a.Layer - b.Layer
}