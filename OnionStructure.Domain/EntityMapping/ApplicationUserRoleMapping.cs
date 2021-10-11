using OnionStructure.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnionStructure.Domain.EntityMapping
{
    public class ApplicationUserRoleMapping : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable("aspnetuserroles");

            builder.HasKey(s => new { s.UserId, s.RoleId });

            builder.HasOne(xr => xr.User)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.UserId);

            builder.HasOne(xr => xr.Role)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.RoleId);
        }
    }
}
