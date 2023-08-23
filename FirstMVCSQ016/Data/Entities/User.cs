using Microsoft.AspNetCore.Identity;

namespace FirstMVCSQ016.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}
