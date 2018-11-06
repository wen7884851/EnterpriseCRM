var Site;
(function (Site) {
    var CMS;
    (function (CMS) {
        var cookie = /** @class */ (function () {
            function cookie() {
            }
            cookie.prototype.setCookie = function (c_name, value) {
                var exdate = new Date();
                document.cookie = c_name + "=" + escape(value);
            };
            cookie.prototype.delCookie = function (name) {
                var self = this;
                var exp = new Date();
                exp.setTime(exp.getTime() - 1);
                var cval = self.getCookie(name);
                if (cval != null)
                    document.cookie = name + "=" + cval + ";expires=" + exp.toString();
            };
            cookie.prototype.getCookie = function (name) {
                var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
                if (arr = document.cookie.match(reg))
                    return unescape(arr[2]);
                else
                    return null;
            };
            return cookie;
        }());
        CMS.cookie = cookie;
    })(CMS = Site.CMS || (Site.CMS = {}));
})(Site || (Site = {}));
//# sourceMappingURL=cookie.js.map