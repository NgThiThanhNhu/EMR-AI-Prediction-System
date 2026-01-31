using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.DocumentTypeConfiguration
{
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Document__3214EC074EED5EB1");

            builder.HasIndex(e => e.Code, "UQ__Document__A25C5AA7F3FC4575").IsUnique();

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.Code).HasMaxLength(20);
            builder.Property(e => e.MaxSignatures).HasDefaultValue(1);
            builder.Property(e => e.Name).HasMaxLength(255);
        }
    }
}





