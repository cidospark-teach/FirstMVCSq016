using FirstMVCSQ016.Data.Entities;
using System.Security.Claims;

namespace FirstMVCSQ016.Services
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(AppUser user, string password);
        Task LogoutAsync();
        bool IsLoggedInAsync(ClaimsPrincipal user);
    }
}
