using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using JobPortal.EntityModel;

namespace JobPortal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Organization> Organization { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<JobDescription> JobDescription { get; set; }


    }
}
