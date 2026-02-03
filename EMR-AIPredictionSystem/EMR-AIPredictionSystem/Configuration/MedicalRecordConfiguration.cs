using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EMR_AIPredictionSystem.Configuration.MedicalRecordConfiguration
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__MedicalR__3214EC0797F202F0");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.AppointmentId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            builder.Property(e => e.DepartmentId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.DoctorId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.MedicalDocumentId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.PaidAt).HasColumnType("datetime");
            builder.Property(e => e.PatientId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("CHUA_THANH_TOAN");
            builder.Property(e => e.RecordStatus)
                .HasMaxLength(20)
                .HasDefaultValue("OPEN");
            builder.Property(e => e.TreatmentStatus).HasMaxLength(50);
            builder.Property(e => e.UpdatedAt).HasColumnType("datetime");
            builder.Property(e => e.VisitAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");

            builder.HasOne(d => d.Appointment).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_Records_Appointment");

            builder.HasOne(d => d.Department).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Records_Department");

            builder.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_Records_Doctor");

            builder.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Records_Patient");

            builder.HasOne(d => d.PaymentConfirmedByUser).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PaymentConfirmedByUserId)
                .HasConstraintName("FK_Records_PaymentUser");

            builder.HasOne(r => r.MedicalDocument)
                .WithOne(d => d.MedicalRecord)
                .HasForeignKey<MedicalRecord>(r => r.MedicalDocumentId)
                .HasConstraintName("FK_Record_Document")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

