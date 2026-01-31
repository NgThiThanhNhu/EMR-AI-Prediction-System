using EMR_AIPredictionSystem.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMR_AIPredictionSystem.Configuration.PatientConfiguration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Patients__3214EC0754B51ECB");

            builder.HasIndex(e => e.InsuranceNumber, "UQ__Patients__01A4DDACB0500304").IsUnique();

            builder.HasIndex(e => e.PersonId, "UQ__Patients__AA2FFBE4F8F27177").IsUnique();

            builder.Property(e => e.Id)
                .HasMaxLength(11)
                .IsUnicode(false);
            //builder.Property(e => e.CreateDate).HasColumnType("datetime");
            //builder.Property(e => e.CreateUser).HasMaxLength(50);
            //builder.Property(e => e.DeleteDate).HasColumnType("datetime");
            //builder.Property(e => e.DeleteUser).HasMaxLength(50);
            builder.Property(e => e.InsuranceNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            //builder.Property(e => e.IsDeleted).HasDefaultValue(false);
            //builder.Property(e => e.UpdateDate).HasColumnType("datetime");
            //builder.Property(e => e.UpdateUser).HasMaxLength(50);

            builder.HasOne(d => d.Person).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Person");
        }
    }
}
