using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DMS.BLL.Models;

public partial class DmsdatabaseContext : DbContext
{
    public DmsdatabaseContext()
    {
    }

    public DmsdatabaseContext(DbContextOptions<DmsdatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blockchain> Blockchains { get; set; }

    public virtual DbSet<Diploma> Diplomas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];
        optionsBuilder.UseSqlServer(strConn);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blockchain>(entity =>
        {
            entity.HasKey(e => e.BlockHash).HasName("PK__Blockcha__894D414B9994E30C");

            entity.ToTable("Blockchain");

            entity.Property(e => e.BlockHash).HasMaxLength(255);
            entity.Property(e => e.DataHash).HasMaxLength(255);
            entity.Property(e => e.PreviousHash).HasMaxLength(255);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Diploma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diplomas__3214EC07BB533D69");

            entity.HasIndex(e => e.DiplomaNumber, "UQ__Diplomas__CD7EA2EFA4A01EFD").IsUnique();

            entity.Property(e => e.BlockchainHash).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiplomaNumber).HasMaxLength(50);
            entity.Property(e => e.Major).HasMaxLength(100);
            entity.Property(e => e.StudentId).HasMaxLength(20);
            entity.Property(e => e.StudentName).HasMaxLength(100);
            entity.Property(e => e.UniversityName).HasMaxLength(200);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Diplomas)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diplomas__Create__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07C45F4087");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4FAEF849B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A570E3B3").IsUnique();

            entity.Property(e => e.AvatarUrl).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("Student");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
