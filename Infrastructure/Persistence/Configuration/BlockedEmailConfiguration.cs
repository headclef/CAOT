using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class BlockedEmailConfiguration : IEntityTypeConfiguration<BlockedEmail>
    {
        #region Methods
        /// <summary>
        /// Configures the BlockedEmail entity.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<BlockedEmail> builder)
        {
            builder.ToTable("BlockedEmail");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.RetryCount).IsRequired();
        }
        #endregion
    }
}