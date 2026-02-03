using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.MedicalFileConfiguration
{
    public class MedicalFileConfiguration : IEntityTypeConfiguration<MedicalFile>
    {
        public void Configure(EntityTypeBuilder<MedicalFile> builder)
        {
            //builder.HasKey(e => e.Id).HasName("PK__MedicalF__3214EC07995FFD74");

            //builder.HasIndex(e => new { e.PatientId, e.Year }, "UQ_Patient_Year").IsUnique();

            //builder.Property(e => e.Id)
            //    .HasMaxLength(11)
            //    .IsUnicode(false);
            //builder.Property(e => e.Status).HasMaxLength(100);
            //builder.Property(e => e.CreatedAt)
            //    .HasDefaultValueSql("(sysdatetime())")
            //    .HasColumnType("datetime");
            //builder.Property(e => e.PatientId)
            //    .HasMaxLength(11)
            //    .IsUnicode(false);

            //builder.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MedicalFiles)
            //    .HasForeignKey(d => d.CreatedBy)
            //    .HasConstraintName("FK_File_User");

            //builder.HasOne(d => d.Patient).WithMany(p => p.MedicalFiles)
            //    .HasForeignKey(d => d.PatientId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_File_Patient");
            builder.ToTable("MedicalFiles");

            builder.HasKey(e => e.Id)
                .HasName("PK__MedicalF__3214EC07995FFD74");

            builder.HasIndex(e => new { e.PatientId, e.Year })
                .IsUnique()
                .HasDatabaseName("UQ_Patient_Year");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);

            builder.Property(e => e.PatientId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.FileCategoryId)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Status)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .IsRequired(false);

            builder.Property(e => e.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("SYSDATETIME()");

            builder.HasOne(d => d.CreatedByNavigation)
                .WithMany(p => p.MedicalFiles)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_File_User");

            builder.HasOne(d => d.Patient)
                .WithMany(p => p.MedicalFiles)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_File_Patient");

            builder.HasOne(d => d.FileCategory)
                .WithMany(p => p.MedicalFiles)
                .HasForeignKey(d => d.FileCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MedicalFiles_FileCategory");
        }
    }
}


