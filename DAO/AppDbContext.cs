namespace Web_1.DAO;
using Web_1;
using Microsoft.EntityFrameworkCore;
using Web_1.Models;

public class AppDbContext : DbContext
{
    public DbSet<HocVien> HocViens { get; set; }
    public DbSet<KhoaHoc> KhoaHocs { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public HocVien? GetHocVienById(int id)
    {
        return HocViens.Include(hv=>hv.KhoaHocs).Include(ac=>ac._account).FirstOrDefault(hv => hv._id == id);
    }
    // Constructor
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.TK)
            .IsUnique();

        modelBuilder.Entity<HocVien>()
            .HasMany(a => a.KhoaHocs)
            .WithMany(h => h._hocViens)
            .UsingEntity(j => j.ToTable("HocVienKhoaHocs"));

        // modelBuilder.Entity<HocVien>()
        //     .HasOne(a => a.Account)
        //     .WithOne(h => h.hocVien)
        //     .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Account>()
            .HasOne(a => a.hocVien)
            .WithOne(h => h._account)
            .HasForeignKey<Account>(a => a.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<KhoaHoc>()
            .HasMany(h => h._hocViens)
            .WithMany(a => a.KhoaHocs)
            .UsingEntity(j => j.ToTable("HocVienKhoaHocs"));
    }

}
