var Site;
(function (Site) {
    var CMS;
    (function (CMS) {
        var LoginView = /** @class */ (function () {
            function LoginView() {
                this.userName = ko.observable();
                this.password = ko.observable();
                this.errorMessage = ko.observable();
                this.progress = ko.observable(false);
                this.canLogin = ko.observable(true);
                this.isMoble = ko.observable(true);
            }
            ;
            LoginView.prototype.CheckLogin = function () {
                var self = this;
                self.canLogin(false);
                if (self.userName.toString() == "" || self.password.toString() == "") {
                    self.errorMessage("用户名和密码不能为空");
                }
                else {
                    var user = {
                        userName: self.userName,
                        password: self.password,
                        isMoble: self.isMoble
                    };
                    self.progress(true);
                    $.post(self.api().checkPassWord, user).then(function (data) {
                        if (!data.isSuccess) {
                            self.errorMessage("用户名和密码错误,请重新输入");
                        }
                        else {
                            self.cookie.setCookie("userInfo", data.content);
                        }
                    }).always(function () { self.progress(false); self.canLogin(true); });
                }
            };
            LoginView.prototype.api = function () {
                var self = this;
                return {
                    checkPassWord: "/Login/CheckLogin"
                };
            };
            return LoginView;
        }());
        CMS.LoginView = LoginView;
        var LoginContent = /** @class */ (function () {
            function LoginContent() {
            }
            return LoginContent;
        }());
        CMS.LoginContent = LoginContent;
    })(CMS = Site.CMS || (Site.CMS = {}));
})(Site || (Site = {}));
//# sourceMappingURL=login.js.map