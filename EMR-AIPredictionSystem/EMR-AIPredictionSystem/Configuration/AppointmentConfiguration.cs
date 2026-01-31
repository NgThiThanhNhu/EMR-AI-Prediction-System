using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.AppointmentConfiguration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Appointm__3214EC079E14A65C");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.AppointmentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("PENDING");
            builder.Property(e => e.AppointmentType)
                .HasMaxLength(20)
                .HasDefaultValue("ONLINE");
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            builder.Property(e => e.PatientId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.ScheduleId)
                .HasMaxLength(11)
                .IsUnicode(false);

            builder.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Patient");

            builder.HasOne(d => d.Schedule).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Schedule");
        }
    }
}




