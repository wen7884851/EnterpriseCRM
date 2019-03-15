namespace Domain.Site.Models
{
    public class ActionResultViewModel
    {
        public bool IsSuccess { get; set; }

        public bool UserTermAccepted { get; set; }

        public bool ShouldChangePassword { get; set; }

        public string Token { get; set; }

        public object Result { get; set; }
    }
}
