using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.DoctorConfiguration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Doctors__3214EC07B95BA96A");

            builder.HasIndex(e => e.UserId, "UQ__Doctors__1788CC4D6BC125D7").IsUnique();

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.DepartmentId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.DigitalSignaturePath).HasMaxLength(500);

            builder.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctors_Department");

            builder.HasOne(d => d.User).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doctors_User");
        }
    }
}







