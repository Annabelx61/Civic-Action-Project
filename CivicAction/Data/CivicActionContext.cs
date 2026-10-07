using Microsoft.EntityFrameworkCore;
using CivicAction.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CivicAction.Data;

public class CivicActionContext : IdentityDbContext<AppUser>
{
    
    public CivicActionContext(DbContextOptions<CivicActionContext> options)
        : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Update> Updates { get; set; }
    public DbSet<Verification> Verifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>().ToTable("Account");
        modelBuilder.Entity<Project>().ToTable("Project");
        modelBuilder.Entity<Update>().ToTable("Update");
        modelBuilder.Entity<Verification>().ToTable("Verification");

        modelBuilder.Entity<Verification>()
            .HasOne(v => v.Admin)
            .WithMany(u => u.AdminVerifications)
            .HasForeignKey(v => v.AdminId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Verification>()
            .HasOne(v => v.Project)
            .WithMany(p => p.Verifications)
            .HasForeignKey(v => v.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Verification>()
            .HasOne(v => v.Student)
            .WithMany(u => u.StudentVerifications)
            .HasForeignKey(v => v.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Update>()
            .HasOne(u => u.Verification)
            .WithMany(v => v.Updates)
            .HasForeignKey(u => u.VerificationId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}