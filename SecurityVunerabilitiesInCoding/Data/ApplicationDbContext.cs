using AdvancedAspnetCoreSecurity.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SecurityVunerabilitiesInCoding.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<SecurityEventLog> SecurityEventLogs { get; set; }
    }
}
