using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.PrescriptionDetailConfiguration
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Prescrip__3214EC0788220966");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            builder.Property(e => e.DiseaseCode).HasMaxLength(50);
            builder.Property(e => e.DiseaseName).HasMaxLength(255);
            builder.Property(e => e.MedicalRecordId)
                .HasMaxLength(11)
                .IsUnicode(false);

            builder.HasOne(d => d.MedicalRecord).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("FK_Prescriptions_Record");
        }
    }
}