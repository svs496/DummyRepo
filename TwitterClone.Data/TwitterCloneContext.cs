using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TwitterClone.Data.Models;

namespace TwitterClone.Data
{ 
    public class TwitterCloneContext : IdentityDbContext<User>
    {
        public TwitterCloneContext(DbContextOptions<TwitterCloneContext> options)
            : base(options)
        {
        }

        public DbSet<Tweet> Tweets { get; set; }

        public DbSet<UserFollower> UserFollowers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName();
            }

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            

        }
    }
}
