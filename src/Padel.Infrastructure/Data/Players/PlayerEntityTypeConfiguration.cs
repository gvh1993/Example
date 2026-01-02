using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padel.Domain.Player;
using Padel.Infrastructure.Data.Shared;

namespace Padel.Infrastructure.Data.Players;

internal sealed class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.DateOfBirth)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion<EmailAddressConversion>()
            .HasMaxLength(500);

        builder.Property(x => x.PhoneNumber)
            .HasConversion<PhoneNumberConversion>()
            .HasMaxLength(50);
    }
}
