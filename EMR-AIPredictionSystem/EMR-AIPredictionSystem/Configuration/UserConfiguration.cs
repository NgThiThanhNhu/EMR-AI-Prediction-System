
using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.UserConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Users__3214EC070092C484");

            builder.HasIndex(e => e.Username, "UQ__Users__536C85E479828C85").IsUnique();

            builder.HasIndex(e => e.PersonId, "UQ__Users__AA2FFBE4B044855A").IsUnique();
            builder.Property(e => e.Email).HasMaxLength(256);
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            //builder.Property(e => e.CreateDate).HasColumnType("datetime");
            //builder.Property(e => e.CreateUser).HasMaxLength(50);
            //builder.Property(e => e.DeleteDate).HasColumnType("datetime");
            //builder.Property(e => e.DeleteUser).HasMaxLength(50);
            //builder.Property(e => e.IsDeleted).HasDefaultValue(false);
            builder.Property(e => e.IsVerified).HasDefaultValue(false);
            builder.Property(e => e.RoleId)
                .HasMaxLength(11)
                .IsUnicode(false);
            //builder.Property(e => e.UpdateDate).HasColumnType("datetime");
            //builder.Property(e => e.UpdateUser).HasMaxLength(50);
            builder.Property(e => e.Username).HasMaxLength(100);

            builder.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Person");

            builder.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        }
    }
}