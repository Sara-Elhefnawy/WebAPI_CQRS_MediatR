using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(b => b.BookingDate).IsRequired();
        builder.Property(b => b.IsAttended).IsRequired();
        builder.Property(b => b.IsDeleted).IsRequired();

        builder.HasIndex(b => new { b.MemberId, b.SessionId }).IsUnique();
    }
}
