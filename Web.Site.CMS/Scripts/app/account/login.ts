namespace Site.CMS {
    export class LoginView {
        public userName = ko.observable<string>();
        public password = ko.observable<string>();
        public errorMessage = ko.observable<string>();
        public progress = ko.observable<boolean>(false);
        public canLogin = ko.observable<boolean>(true);
        public isMoble = ko.observable<boolean>(true);
        constructor() { };

        CheckLogin() {
            let self = this;
            self.canLogin(false);
            if (self.userName.toString() == "" || self.password.toString() == "") {
                self.errorMessage("用户名和密码不能为空");
            }
            else {
                let user = {
                    userName: self.userName,
                    password: self.password,
                    isMoble: self.isMoble
                };
                self.progress(true);
                $.post(self.api().checkPassWord, user).then((data: LoginContent) => {
                    if (!data.isSuccess) {
                        self.errorMessage("用户名和密码错误,请重新输入");
                    }
                }).always(function () { self.progress(false); self.canLogin(true); })
            }
        }

        public api() {
            var self = this;
            return {
                checkPassWord: "/Login/CheckLogin"
            }
        }

    }


    export class LoginContent {
        isSuccess: boolean;
        content: string;
    }
}