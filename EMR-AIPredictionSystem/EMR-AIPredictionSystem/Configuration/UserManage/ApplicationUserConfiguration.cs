using EMR_AIPredictionSystem.Model.Entities.UserManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.UserManage
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username)
                .HasMaxLength(200);
            builder.Property(u => u.Email)
                .HasMaxLength(100);
            builder.Property(u => u.Password)
                .HasMaxLength(255);
            builder.HasOne(u => u.Role)
                .WithMany(r => r.applicationUsers)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
