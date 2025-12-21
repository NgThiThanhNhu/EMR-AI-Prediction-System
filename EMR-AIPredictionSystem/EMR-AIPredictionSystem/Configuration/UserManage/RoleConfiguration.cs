using EMR_AIPredictionSystem.Model.Entities.UserManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.UserManage
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(r => r.RoleName)
                .HasMaxLength(128);
        }
    }
}
