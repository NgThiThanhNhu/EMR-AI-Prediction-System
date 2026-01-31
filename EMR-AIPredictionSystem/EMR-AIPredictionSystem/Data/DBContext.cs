//using System;
//using System.Collections.Generic;
//using EMR_AIPredictionSystem.Model.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace EMR_AIPredictionSystem.Data;

//public partial class DBContext : DbContext
//{
//    public DBContext()
//    {
//    }

//    public DBContext(DbContextOptions<DBContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Aidiagnosis> Aidiagnoses { get; set; }

//    public virtual DbSet<Appointment> Appointments { get; set; }

//    public virtual DbSet<ClinicalVital> ClinicalVitals { get; set; }

//    public virtual DbSet<Department> Departments { get; set; }

//    public virtual DbSet<DigitalSignature> DigitalSignatures { get; set; }

//    public virtual DbSet<Doctor> Doctors { get; set; }

//    public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }

//    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

//    public virtual DbSet<LabResult> LabResults { get; set; }

//    public virtual DbSet<MedicalDocument> MedicalDocuments { get; set; }

//    public virtual DbSet<MedicalFile> MedicalFiles { get; set; }

//    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

//    public virtual DbSet<Notification> Notifications { get; set; }

//    public virtual DbSet<Patient> Patients { get; set; }

//    public virtual DbSet<Person> Persons { get; set; }

//    public virtual DbSet<Prescription> Prescriptions { get; set; }

//    public virtual DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }

//    public virtual DbSet<Role> Roles { get; set; }

//    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-3A6OS2F\\SQLEXPRESS;Database=EMR_AI;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true;");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {

//        modelBuilder.Entity<User>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC070092C484");

//            entity.HasIndex(e => e.Username, "UQ__Users__536C85E479828C85").IsUnique();

//            entity.HasIndex(e => e.PersonId, "UQ__Users__AA2FFBE4B044855A").IsUnique();

//            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
//            entity.Property(e => e.CreateDate).HasColumnType("datetime");
//            entity.Property(e => e.CreateUser).HasMaxLength(50);
//            entity.Property(e => e.DeleteDate).HasColumnType("datetime");
//            entity.Property(e => e.DeleteUser).HasMaxLength(50);
//            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
//            entity.Property(e => e.IsVerified).HasDefaultValue(false);
//            entity.Property(e => e.RoleId)
//                .HasMaxLength(11)
//                .IsUnicode(false);
//            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
//            entity.Property(e => e.UpdateUser).HasMaxLength(50);
//            entity.Property(e => e.Username).HasMaxLength(100);

//            entity.HasOne(d => d.Person).WithOne(p => p.User)
//                .HasForeignKey<User>(d => d.PersonId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Users_Person");

//            entity.HasOne(d => d.Role).WithMany(p => p.Users)
//                .HasForeignKey(d => d.RoleId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_Users_Roles");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
