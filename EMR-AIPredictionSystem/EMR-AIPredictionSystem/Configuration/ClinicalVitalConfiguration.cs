using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.ClinicalVitalConfiguration
{
    public class ClinicalVitalConfiguration : IEntityTypeConfiguration<ClinicalVital>
    {
        public void Configure(EntityTypeBuilder<ClinicalVital> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Clinical__3214EC07B10FACE7");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.BloodPressure).HasMaxLength(20);
            builder.Property(e => e.MedicalRecordId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.RecordedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");

            builder.HasOne(v => v.MedicalRecord)
            .WithOne(r => r.ClinicalVital)
            .HasForeignKey<ClinicalVital>(v => v.MedicalRecordId)
            .HasConstraintName("FK_Vital_Record");
        }
    }
}



