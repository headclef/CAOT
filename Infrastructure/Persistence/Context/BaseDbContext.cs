using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence.Context
{
    public class BaseDbContext : DbContext
    {
        #region Properties
        public DbSet<User> Users { get; set; } = null!;                     // User entity
        public DbSet<BlockedEmail> BlockedEmails { get; set; } = null!;     // BlockedEmail entity
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for BaseDbContext that takes DbContextOptions as a parameter.
        /// </summary>
        /// <param name="options"></param>
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }
        #endregion
        #region Methods
        /// <summary>
        /// Configures the models for the context.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method
            base.OnModelCreating(modelBuilder);

            // Apply the configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BlockedEmailConfiguration());
        }
        #endregion
    }
}