using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.LabResultConfiguration
{
    public class LabResultConfiguration : IEntityTypeConfiguration<LabResult>
    {
        public void Configure(EntityTypeBuilder<LabResult> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__LabResul__3214EC0769EC993A");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.Evaluation).HasMaxLength(20);
            builder.Property(e => e.MedicalRecordId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.ReferenceRange).HasMaxLength(50);
            builder.Property(e => e.ResultValue).HasMaxLength(50);
            builder.Property(e => e.TestName).HasMaxLength(100);
            builder.Property(e => e.Unit).HasMaxLength(20);

            builder.HasOne(d => d.MedicalRecord).WithMany(p => p.LabResults)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("FK_Lab_Record");
        }
    }
}




