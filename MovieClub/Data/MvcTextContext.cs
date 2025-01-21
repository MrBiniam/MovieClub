using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieClub.Models;

namespace MovieClub.Data
{
    public class MvcTextContext : IdentityDbContext
    {
        public MvcTextContext(DbContextOptions<MvcTextContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
