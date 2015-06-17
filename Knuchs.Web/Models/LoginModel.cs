namespace Knuchs.Web.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool HasError { get; set; }
        public string ErroMessage { get; set; }

        //Set Cookie with Hashed Password and Username
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}