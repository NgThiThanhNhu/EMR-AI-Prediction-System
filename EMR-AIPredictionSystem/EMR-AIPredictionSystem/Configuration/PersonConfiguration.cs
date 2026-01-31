using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.PatientConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Persons__3214EC07FBE46978");

            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.AvatarPath).HasMaxLength(500);
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            builder.Property(p => p.DateOfBirth)
            .HasColumnType("date");
            //builder.Property(e => e.Email).HasMaxLength(256);
            builder.Property(e => e.FullName).HasMaxLength(100);
            builder.Property(e => e.Gender).HasDefaultValue(true);
            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
        }
    }
}
