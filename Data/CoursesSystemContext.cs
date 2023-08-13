using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using online_course_platform.Models;

namespace online_course_platform.Data;

public partial class CoursesSystemContext : DbContext
{
    public CoursesSystemContext()
    {
    }

    public CoursesSystemContext(DbContextOptions<CoursesSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Teach> Teaches { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddJsonFile("appsettings.Production.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DB");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CourseName)
                .IsUnicode(false)
                .HasColumnName("Course_Name");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("End_Date");
            entity.Property(e => e.IpAddressOfLastUpdate)
                .IsUnicode(false)
                .HasColumnName("IP_Address_Of_Last_Update");
            entity.Property(e => e.LastUpdateOperation)
                .IsUnicode(false)
                .HasColumnName("Last_Update_Operation");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("Start_Date");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CourseId });

            entity.ToTable("Enrollment");

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.IpAddressOfLastUpdate)
                .IsUnicode(false)
                .HasColumnName("IP_Address_Of_Last_Update");
            entity.Property(e => e.LastUpdateOperation)
                .IsUnicode(false)
                .HasColumnName("Last_Update_Operation");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Enrollment_Course");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Enrollment_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId });

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.IpAddressOfLastUpdate)
                .IsUnicode(false)
                .HasColumnName("IP_Address_Of_Last_Update");
            entity.Property(e => e.LastUpdateOperation)
                .IsUnicode(false)
                .HasColumnName("Last_Update_Operation");
            entity.Property(e => e.RoleType)
                .IsUnicode(false)
                .HasColumnName("Role_Type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_Role_User");
        });

        modelBuilder.Entity<Teach>(entity =>
        {
            entity.HasKey(e => e.CourseId);

            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("Course_ID");
            entity.Property(e => e.IpAddressOfLastUpdate)
                .IsUnicode(false)
                .HasColumnName("IP_Address_Of_Last_Update");
            entity.Property(e => e.LastUpdateOperation)
                .IsUnicode(false)
                .HasColumnName("Last_Update_Operation");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.Course).WithOne(p => p.Teach)
                .HasForeignKey<Teach>(d => d.CourseId)
                .HasConstraintName("FK_Teaches_Course_ID");

            entity.HasOne(d => d.CourseNavigation).WithOne(p => p.Teach)
                .HasForeignKey<Teach>(d => d.CourseId)
                .HasConstraintName("FK_Teaches_Instructor_ID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.UserName, "Index_User_UserName").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("Birth_Date");
            entity.Property(e => e.FirstName)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.Gender).HasComment("1 --> male, 0 --> female");
            entity.Property(e => e.IpAddressOfLastUpdate)
                .IsUnicode(false)
                .HasColumnName("IP_Address_Of_Last_Update");
            entity.Property(e => e.LastName)
                .IsUnicode(false)
                .HasColumnName("Last_Name");
            entity.Property(e => e.LastUpdateOperation)
                .IsUnicode(false)
                .HasColumnName("Last_Update_Operation");
            entity.Property(e => e.PasswordHash)
                .IsUnicode(false)
                .HasColumnName("Password_Hash");
            entity.Property(e => e.PasswordHashSalt)
                .IsUnicode(false)
                .HasColumnName("Password_Hash_Salt");
            entity.Property(e => e.UpdateBy).HasColumnName("Update_By");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("User_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
