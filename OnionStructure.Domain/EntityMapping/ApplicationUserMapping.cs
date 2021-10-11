using OnionStructure.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnionStructure.Domain.EntityMapping
{
    public class ApplicationUserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("aspnetusers");

            builder.HasKey(s => s.Id);
            builder.Property(s=> s.Id)
                .HasColumnType("VARCHAR")
                .HasMaxLength(128);

            builder.Property(p => p.FirstName)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);


            builder.Property(p => p.LastName)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(p => p.EmployeeBadgeId)
               .HasColumnType("VARCHAR")
               .HasMaxLength(256);
        }
    }
}
