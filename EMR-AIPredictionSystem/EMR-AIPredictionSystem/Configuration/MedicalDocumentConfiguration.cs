using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.MedicalDocumentConfiguration
{
    public class MedicalDocumentConfiguration : IEntityTypeConfiguration<MedicalDocument>
    {
        public void Configure(EntityTypeBuilder<MedicalDocument> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__MedicalD__3214EC07CC7A121F");

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            builder.Property(e => e.DocumentTypeId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.MedicalFileId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValue("DRAFT");

            builder.HasOne(d => d.DocumentType).WithMany(p => p.MedicalDocuments)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doc_Type");

            builder.HasOne(d => d.MedicalFile).WithMany(p => p.MedicalDocuments)
                .HasForeignKey(d => d.MedicalFileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Doc_File");
        }
    }
}



