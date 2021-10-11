using OnionStructure.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnionStructure.Domain.EntityMapping
{
    public class ApplicationRoleMapping : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("aspnetroles");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(128);

        }
    }
}
