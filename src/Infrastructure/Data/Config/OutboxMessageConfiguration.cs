using Infrastructure.Data.Outbox;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.Property(t => t.Type).IsRequired();
        builder.Property(t => t.Content).IsRequired();
        builder.Property(t => t.OcurredOnUtc).IsRequired();
    }
}
