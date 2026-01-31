using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.AidiagnosisConfiguration
{
    public class AidiagnosisConfiguration : IEntityTypeConfiguration<Aidiagnosis>
    {
        public void Configure(EntityTypeBuilder<Aidiagnosis> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__AIDiagno__3214EC07DEB516FE");

            builder.ToTable("AIDiagnoses");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            builder.Property(e => e.MedicalRecordId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.PredictedDisease).HasMaxLength(255);
            builder.Property(e => e.Priority).HasMaxLength(20);

            builder.HasOne(d => d.MedicalRecord).WithMany(p => p.Aidiagnoses)
                .HasForeignKey(d => d.MedicalRecordId)
                .HasConstraintName("FK_AI_Record");
        }
    }
}




