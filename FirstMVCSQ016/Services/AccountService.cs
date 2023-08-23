using FirstMVCSQ016.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FirstMVCSQ016.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AccountService(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        
        public async Task<bool> LoginAsync(AppUser user, string password)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if(loginResult.Succeeded)
                return true;

            return false;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public bool IsLoggedInAsync(ClaimsPrincipal user)
        {
            if (_signInManager.IsSignedIn(user)) { return true; }
            return false;
        }

    }
}
