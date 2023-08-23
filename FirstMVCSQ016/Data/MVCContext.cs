using FirstMVCSQ016.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCSQ016.Data
{
    public class MVCContext : IdentityDbContext<AppUser>
    {
        public MVCContext(DbContextOptions<MVCContext> options): base(options)
        {
                
        }
    }
}
