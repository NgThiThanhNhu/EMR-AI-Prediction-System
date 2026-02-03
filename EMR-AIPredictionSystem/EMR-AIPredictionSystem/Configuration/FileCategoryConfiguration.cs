using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration
{
    public class FileCategoryConfiguration : IEntityTypeConfiguration<FileCategory>
    {
        public void Configure(EntityTypeBuilder<FileCategory> builder)
        {
            builder.ToTable("FileCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsUnicode()
                .IsRequired();

            // Relationship: FileCategory (1) - MedicalFile (N)
            builder.HasMany(x => x.MedicalFiles)
                .WithOne(x => x.FileCategory)
                .HasForeignKey(x => x.FileCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
