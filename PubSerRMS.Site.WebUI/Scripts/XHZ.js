zishiyin = function (md, zt) {
    var dw = document.body.clientWidth
    var dh = document.body.clientHeight
    var width = document.getElementById("padiv").style.width;
    var w = toPoint(width)
    var cw = dw * w
    var height = document.getElementById("padiv").style.height;
    var h = toPoint(height)
    var ch = dh * h


    if (dw < dh) {
        $("#padiv div").addClass("zidiv2")
        var k = cw * 0.35
        var g = ch * 0.3
        var w1 = dw * 0.65
        var w2 = dh * 1
        //$("#padiv").css({ "width": w1, "height": w2, "padding-top": k / 2.5 })
        //$("#padiv div a").css({ "width": k / 1.1, "height": k / 1.6, "padding-top": k /8 })
        //$("#padiv div").css({ "width": k / 1.1, "height": k / 1.3, "margin": "5px 0 0 12px", "font-size": "2.5rem" })
        $("#padiv div a").css({ "width": k / 1.07, "height": k / 1.55, "padding-top": k / 8 })
        $("#padiv div").css({ "width": k / 1.07, "height": k / 1.3, "margin": "-2px 0 0 3.7px", "font-size": "2.5rem" })
        //$("")
        //$("#headdiv").css({"height": "40px" })
    }
    if (dw >= dh) {
        $("#padiv div").addClass("zidiv1")
        var k = cw * 0.2
        var g = ch * 0.4
        var mianji = k * g
        //$("#padiv").css({ "width": dw * 0.5, "height": dh * 0.79, "padding-top": k / 2.5 })
        $("#padiv div").css({ "width": k, "height": k, "margin": "25px 0 0 25px" })
        $("#padiv div a").css({ "width": k, "height": k / 1.7, "padding-top": k / 2.5 })
        //$("#headdiv").css({ "height": "60px" })
        if (zt == "ax") {
            var k = cw * 0.2
            var g = ch * 0.4
            var w1 = k / 1.3
            var w2 = k / 2
            $("#padiv div a[href=" + md + "]").css({ "width": w1, "height": w2, "position": "relative", "top": "35px", "left": "35px", "padding-top": k / 4 })
            $("#padiv div a[href=" + md + "]").parent().css({ "background-color": "rgba(255, 255, 255, 0.00)", "border": "1px solid rgba(255, 255, 255, 0.00)" })
        }

        if (zt == "skl") {
            var k = cw * 0.2
            var g = ch * 0.4
            $("#padiv div a[href=" + md + "]").parent().css({ "background-color": "#5688a5", "border": "1px solid #5688a5" })
            $("#padiv div").css({ "width": k, "height": k, "margin": "25px 0 0 25px" })
            $("#padiv div a[href=" + md + "]").css({ "width": k, "height": k / 1.7, "padding-top": k / 2.5, "position": "relative", "top": "0", "left": "0" })
        }
        if (zt == "skw") {
            var k = cw * 0.2
            var g = ch * 0.4
            //$("#padiv div").children().addClass("ac")
            $("#padiv div").css({ "background-color": "#5688a5", "border": "1px solid #5688a5" })
            $("#padiv div").css({ "width": k, "height": k, "margin": "25px 0 0 25px" })
            $("#padiv div").children().css({ "width": k, "height": k / 1.7, "padding-top": k / 2.5, "position": "relative", "top": "0", "left": "0" })
        }
    }
}
window.onresize = function () {
    zishiyin()
}
function toPoint(percent) {
    var str = percent.replace("%", "");
    str = str / 100;
    return str;
}
$(function () {
    zishiyin()
})
acs = function (href, zt) {
    var str = href, indexArr = [];
    for (var i = 0, len = str.length; i < len; i++) {
        if (str[i] === "#") indexArr.push(i)
    }
    var md = str.substring(indexArr[0], str.length)
    zishiyin(md, zt)

    
}


