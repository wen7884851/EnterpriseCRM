function setProportionValue(obj) {
    obj = clearNoNum(obj);
    if (obj > 100) {
        let o = obj.toString();
        obj = parseFloat(o.substr(0, o.length - 1));
    }
    if (obj <= 0) {
        obj = 0;
    }
    return obj;
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

function getParam(paramName){
    let reg = new RegExp("(^|&)" + paramName + "=([^&]*)(&|$)");
    let r = window.location.search.substr(1).match(reg);//search,查询？后面的参数，并匹配正则
    if (r !== null) return unescape(r[2]); return null;
}


// 不对图片进行压缩，直接转成base64
function directTurnIntoBase64(fileObj, callback) {
    var r = new FileReader();
    // 转成base64
    r.onload = function () {
        //变成字符串
        let imgBase64 = r.result;
        console.log(imgBase64);
        callback(imgBase64);
    };
    r.readAsDataURL(fileObj);    //转成Base64格式
}

// 对图片进行压缩
function compress(fileObj, callback) {
    if (typeof (FileReader) === 'undefined') {
        console.log("当前浏览器内核不支持base64图标压缩");
        //调用上传方式不压缩  
        directTurnIntoBase64(fileObj, callback);
    } else {
        try {
            var reader = new FileReader();
            reader.onload = function (e) {
                var image = $('<img/>');
                image.load(function () {
                    square = 700,   //定义画布的大小，也就是图片压缩之后的像素
                        canvas = document.createElement('canvas'),
                        context = canvas.getContext('2d'),
                        imageWidth = 0,    //压缩图片的大小
                        imageHeight = 0,
                        offsetX = 0,
                        offsetY = 0,
                        data = '';

                    canvas.width = square;
                    canvas.height = square;
                    context.clearRect(0, 0, square, square);

                    if (this.width > this.height) {
                        imageWidth = Math.round(square * this.width / this.height);
                        imageHeight = square;
                        offsetX = - Math.round((imageWidth - square) / 2);
                    } else {
                        imageHeight = Math.round(square * this.height / this.width);
                        imageWidth = square;
                        offsetY = - Math.round((imageHeight - square) / 2);
                    }
                    context.drawImage(this, offsetX, offsetY, imageWidth, imageHeight);
                    var data = canvas.toDataURL('image/jpeg');
                    //压缩完成执行回调  
                    callback(data);
                });
                image.attr('src', e.target.result);
            };
            reader.readAsDataURL(fileObj);
        } catch (e) {
            console.log("压缩失败!");
            //调用直接上传方式  不压缩 
            directTurnIntoBase64(fileObj, callback);
        }
    }
}