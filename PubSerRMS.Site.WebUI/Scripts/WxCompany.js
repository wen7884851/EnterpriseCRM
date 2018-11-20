function SeachClick()
{
    $("#PlatformName").keyup(function () {
        $(this).val($(this).val().replace(/[^0-9Xx]/g, ''));
    }).bind("paste", function () {  //CTR+V事件处理  
        $(this).val($(this).val().replace(/[^0-9Xx]/g, ''));
    })

    $("#PlatformName").keydown(function (e) {
        var curKey = e.which;
        if (curKey == 13) {
            __doPostBack('BtnSearch', '');
            return false;
        }
    });
}


//$(function () {
//    $('#divPane').scrollTop($('#divPane').height() + 30);

//    //$("#fileImg").change(function () {

//    //    if ($(this).val()) {//选择了图片上传的时候，异步提交
//    //        alert($(this).val());
//    //        $("#from11").submit();
//    //    }
//    //});
//});