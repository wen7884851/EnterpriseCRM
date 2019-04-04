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