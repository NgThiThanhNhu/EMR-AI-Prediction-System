using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.PrescriptionDetailConfiguration
{
    public class PrescriptionDetailConfiguration : IEntityTypeConfiguration<PrescriptionDetail>
    {
        public void Configure(EntityTypeBuilder<PrescriptionDetail> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Prescrip__3214EC07B4254479");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.AfternoonDose).HasDefaultValue(0.0);
            builder.Property(e => e.Dosage).HasMaxLength(100);
            builder.Property(e => e.EveningDose).HasDefaultValue(0.0);
            builder.Property(e => e.MedicineCategory).HasMaxLength(100);
            builder.Property(e => e.MedicineName).HasMaxLength(100);
            builder.Property(e => e.MorningDose).HasDefaultValue(0.0);
            builder.Property(e => e.NightDose).HasDefaultValue(0.0);
            builder.Property(e => e.Note).HasMaxLength(255);
            builder.Property(e => e.PrescriptionId)
                .HasMaxLength(11)
                .IsUnicode(false);

            builder.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionDetails)
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("FK_PrescriptionDetails_Prescription");
        }
    }
}