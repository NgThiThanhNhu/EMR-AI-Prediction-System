using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.DoctorScheduleConfiguration
{
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__DoctorSc__3214EC07282614EA");

            builder.HasIndex(e => new { e.DoctorId, e.WorkDate, e.Shift }, "UQ_DoctorSchedule").IsUnique();

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.BookedPatients).HasDefaultValue(0);
            builder.Property(e => e.DoctorId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.IsAvailable).HasDefaultValue(true);
            builder.Property(e => e.MaxPatients).HasDefaultValue(20);
            builder.Property(e => e.Shift).HasMaxLength(20);

            builder.HasOne(d => d.Doctor).WithMany(p => p.DoctorSchedules)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_Doctor");
        }
    }
}






