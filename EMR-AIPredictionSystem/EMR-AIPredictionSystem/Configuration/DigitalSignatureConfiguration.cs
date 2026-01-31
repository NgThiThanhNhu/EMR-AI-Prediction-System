using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.DigitalSignatureConfiguration
{
    public class DigitalSignatureConfiguration : IEntityTypeConfiguration<DigitalSignature>
    {
        public void Configure(EntityTypeBuilder<DigitalSignature> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__DigitalS__3214EC077F833DAB");

            builder.HasIndex(e => new { e.DocumentId, e.SignedBy }, "UQ_Document_Signer").IsUnique();

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.DocumentId)
                .HasMaxLength(11)
                .IsUnicode(false);
            builder.Property(e => e.SignaturePath).HasMaxLength(500);
            builder.Property(e => e.SignedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");

            builder.HasOne(d => d.Document).WithMany(p => p.DigitalSignatures)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK_Sign_Document");

            builder.HasOne(d => d.SignedByNavigation).WithMany(p => p.DigitalSignatures)
                .HasForeignKey(d => d.SignedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sign_User");
        }
    }
}

