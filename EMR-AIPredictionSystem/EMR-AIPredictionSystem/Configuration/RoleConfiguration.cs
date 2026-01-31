using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.RoleConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Roles__3214EC07CB643F5B");

            builder.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160F26DA347").IsUnique();

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.CreateDate).HasColumnType("datetime");
            builder.Property(e => e.CreateUser).HasMaxLength(50);
            builder.Property(e => e.DeleteDate).HasColumnType("datetime");
            builder.Property(e => e.DeleteUser).HasMaxLength(50);
            builder.Property(e => e.IsDeleted).HasDefaultValue(false);
            builder.Property(e => e.RoleName).HasMaxLength(50);
            builder.Property(e => e.UpdateDate).HasColumnType("datetime");
            builder.Property(e => e.UpdateUser).HasMaxLength(50);
        }
    }
}