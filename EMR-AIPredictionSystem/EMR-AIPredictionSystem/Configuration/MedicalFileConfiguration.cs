using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.MedicalFileConfiguration
{
    public class MedicalFileConfiguration : IEntityTypeConfiguration<MedicalFile>
    {
        public void Configure(EntityTypeBuilder<MedicalFile> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__MedicalF__3214EC07995FFD74");

            builder.HasIndex(e => new { e.PatientId, e.Year }, "UQ_Patient_Year").IsUnique();

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            builder.Property(e => e.PatientId)
                .HasMaxLength(11)
                .IsUnicode(false);

            builder.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MedicalFiles)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_File_User");

            builder.HasOne(d => d.Patient).WithMany(p => p.MedicalFiles)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_File_Patient");
        }
    }
}


