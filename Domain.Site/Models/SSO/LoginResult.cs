namespace Domain.Site.Models.SSO
{
    public class LoginResult
    {
        public bool Success;
        public string ErrorMsg;
        public string ReturnUrl;
        public string Token;
    }


    public class SubSystem
    {
        public string SystemGid { get; set; }
        public int SubUserID { get; set; }
        public string SubUserName { get; set; }
    }
}