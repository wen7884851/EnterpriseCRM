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

function GetErrorMsg(obj) {
    let result = JSON.parse(obj);
    let msg = '';
    if (result.Result) {
        msg = result.Result;
        return msg;
    }
    msg = result.Message;
    return msg;
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

function GUID() {
    this.date = new Date();   /* 判断是否初始化过，如果初始化过以下代码，则以下代码将不再执行，实际中只执行一次 */
    if (typeof this.newGUID !== 'function') {   /* 生成GUID码 */
        GUID.prototype.newGUID = function () {
            this.date = new Date(); var guidStr = '';
            sexadecimalDate = this.hexadecimal(this.getGUIDDate(), 16);
            sexadecimalTime = this.hexadecimal(this.getGUIDTime(), 16);
            for (var i = 0; i < 9; i++) {
                guidStr += Math.floor(Math.random() * 16).toString(16);
            }
            guidStr += sexadecimalDate;
            guidStr += sexadecimalTime;
            while (guidStr.length < 32) {
                guidStr += Math.floor(Math.random() * 16).toString(16);
            }
            return this.formatGUID(guidStr);
        }
        /* * 功能：获取当前日期的GUID格式，即8位数的日期：19700101 * 返回值：返回GUID日期格式的字条串 */
        GUID.prototype.getGUIDDate = function () {
            return this.date.getFullYear() + this.addZero(this.date.getMonth() + 1) + this.addZero(this.date.getDay());
        }
        /* * 功能：获取当前时间的GUID格式，即8位数的时间，包括毫秒，毫秒为2位数：12300933 * 返回值：返回GUID日期格式的字条串 */
        GUID.prototype.getGUIDTime = function () {
            return this.addZero(this.date.getHours()) + this.addZero(this.date.getMinutes()) + this.addZero(this.date.getSeconds()) + this.addZero(parseInt(this.date.getMilliseconds() / 10));
        }
        /* * 功能: 为一位数的正整数前面添加0，如果是可以转成非NaN数字的字符串也可以实现 * 参数: 参数表示准备再前面添加0的数字或可以转换成数字的字符串 * 返回值: 如果符合条件，返回添加0后的字条串类型，否则返回自身的字符串 */
        GUID.prototype.addZero = function (num) {
            if (Number(num).toString() !== 'NaN' && num >= 0 && num < 10) {
                return '0' + Math.floor(num);
            } else {
                return num.toString();
            }
        }
        /*  * 功能：将y进制的数值，转换为x进制的数值 * 参数：第1个参数表示欲转换的数值；第2个参数表示欲转换的进制；第3个参数可选，表示当前的进制数，如不写则为10 * 返回值：返回转换后的字符串 */GUID.prototype.hexadecimal = function (num, x, y) {
            if (y !== undefined) { return parseInt(num.toString(), y).toString(x); }
            else { return parseInt(num.toString()).toString(x); }
        }
        /* * 功能：格式化32位的字符串为GUID模式的字符串 * 参数：第1个参数表示32位的字符串 * 返回值：标准GUID格式的字符串 */
        GUID.prototype.formatGUID = function (guidStr) {
            var str1 = guidStr.slice(0, 8) + '-', str2 = guidStr.slice(8, 12) + '-', str3 = guidStr.slice(12, 16) + '-', str4 = guidStr.slice(16, 20) + '-', str5 = guidStr.slice(20);
            return str1 + str2 + str3 + str4 + str5;
        }
    }
}

function checkPoneAvailable(str) {
    var myreg = /^[1][3,4,5,7,8][0-9]{9}$/;
    if (myreg.test(str)) {
        return true;
    }
    return false;
}

function checkEmail(str) {
    var re = /^[A-Za-z\d]+([-_.][A-Za-z\d]+)*@([A-Za-z\d]+[-.])+[A-Za-z\d]{2,4}$/;
    if (re.test(str)) {
        return true;
    }
    return false;
}

function checkCardNo(str) {
    var reg = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
    if (reg.test(str)) {
        return true;
    }
    return false;
}  