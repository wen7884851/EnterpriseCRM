//namespace Site.CMS {
//    export class cookie {
//        public setCookie(c_name, value) {
//            var exdate = new Date()
//            document.cookie = c_name + "=" + escape(value);
//        }

//        public delCookie(name) {
//            let self = this;
//            var exp = new Date();
//            exp.setTime(exp.getTime() - 1);
//            var cval = self.getCookie(name);
//            if (cval != null)
//                document.cookie = name + "=" + cval + ";expires=" + exp.toString();
//        }

//        public getCookie(name) {
//            var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
//            if (arr = document.cookie.match(reg))
//                return unescape(arr[2]);
//            else
//                return null;
//        }
//    }
//}