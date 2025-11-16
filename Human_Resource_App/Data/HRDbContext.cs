using System;
using System.Collections.Generic;
using Human_Resource_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_App.Data;

public partial class HRDbContext : DbContext
{
    public HRDbContext()
    {
    }

    public HRDbContext(DbContextOptions<HRDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<GradeHistory> GradeHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HR_DB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grades__3213E83FA9A9729F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<GradeHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GradeHis__3213E83FD804A204");

            entity.ToTable("GradeHistory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedOn).HasColumnName("assigned_on");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.GradeId).HasColumnName("grade_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.GradeHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__GradeHist__emplo__59063A47");

            entity.HasOne(d => d.Grade).WithMany(p => p.GradeHistories)
                .HasForeignKey(d => d.GradeId)
                .HasConstraintName("FK__GradeHist__grade__59FA5E80");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Users__C52E0BA80D6249AC");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__A1936A6BB46A1A1B").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.AccessGranted)
                .HasDefaultValue(true)
                .HasColumnName("access_granted");
            entity.Property(e => e.CurrentGradeId).HasColumnName("current_grade_id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("role");

            entity.HasOne(d => d.CurrentGrade).WithMany(p => p.Users)
                .HasForeignKey(d => d.CurrentGradeId)
                .HasConstraintName("FK__Users__current_g__52593CB8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
