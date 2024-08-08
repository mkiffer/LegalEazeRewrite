using LegalEazeRewrite.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LegalEazeRewrite.Data;
public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<LawFirm> LawFirms { get; set; }
    public DbSet<Lawyer> Lawyers { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<LawyerClient> LawyerClients { get; set; }
    public DbSet<Matter> Matters { get; set; }
    public DbSet<Court> Courts { get; set; }
    public DbSet<LawyerManager> LawyerManagers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // LawyerClient configuration
        modelBuilder.Entity<LawyerClient>()
            .HasKey(lc => new { lc.LawyerID, lc.ClientID });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientID);
            entity.Property(e => e.ClientID).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Court>(entity =>
        {
            entity.HasKey(e => e.CourtID);
            entity.Property(e => e.CourtID).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<LawFirm>(entity =>
        {
            entity.HasKey(e => e.LawFirmID);
            entity.Property(e => e.LawFirmID).ValueGeneratedOnAdd();
        });

        

        modelBuilder.Entity<LawyerClient>()
            .HasOne(lc => lc.Lawyer)
            .WithMany(l => l.LawyerClients)
            .HasForeignKey(lc => lc.LawyerID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<LawyerClient>()
            .HasOne(lc => lc.Client)
            .WithMany(c => c.LawyerClients)
            .HasForeignKey(lc => lc.ClientID)
            .OnDelete(DeleteBehavior.NoAction);

        // LawyerManager configuration
        modelBuilder.Entity<LawyerManager>()
            .HasKey(lm => new { lm.LawyerID, lm.ManagerID });

        modelBuilder.Entity<LawyerManager>()
            .HasOne(lm => lm.Lawyer)
            .WithMany(l => l.LawyerManagers)
            .HasForeignKey(lm => lm.LawyerID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<LawyerManager>()
            .HasOne(lm => lm.Manager)
            .WithMany(m => m.LawyerManagers)
            .HasForeignKey(lm => lm.ManagerID)
            .OnDelete(DeleteBehavior.NoAction);

        // Relationships
        modelBuilder.Entity<Lawyer>()
            .HasOne(l => l.User)
            .WithOne(u => u.Lawyer)
            .HasForeignKey<Lawyer>(l => l.UserID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Manager>()
            .HasOne(m => m.User)
            .WithOne(u => u.Manager)
            .HasForeignKey<Manager>(m => m.UserID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Lawyer>()
            .HasOne(l => l.LawFirm)
            .WithMany(lf => lf.Lawyers)
            .HasForeignKey(l => l.LawFirmID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Manager>()
            .HasOne(m => m.LawFirm)
            .WithMany(lf => lf.Managers)
            .HasForeignKey(m => m.LawFirmID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Matter>()
            .HasOne(m => m.Client)
            .WithMany(c => c.Matters)
            .HasForeignKey(m => m.ClientID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Matter>()
            .HasOne(m => m.Court)
            .WithMany(c => c.Matters)
            .HasForeignKey(m => m.CourtID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
