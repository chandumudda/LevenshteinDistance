
namespace LD.Models
{
    public class AuthenticationDetails
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ClaimSubject { get; set; }
        public string ClaimName { get; set; }
    }
}
