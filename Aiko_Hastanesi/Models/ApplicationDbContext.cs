using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiko_Hastanesi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hoca> Hocas { get; set; }
        public DbSet<Asistan> Asistans { get; set; }
        public DbSet<Bolum> Bolums { get; set; }
        public DbSet<Acil> Acils { get; set; }
        public DbSet<Nobet> Nobets { get; set; }
        public DbSet<HocaMusaitlik> HocaMusaitliks { get; set; }
        public DbSet<Randevu> Randevus {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hoca>().HasData(
                new Hoca { Id = 1, AdSoyad = "Ali Yılmaz", Telefon = "1234567890", Email = "ali.yilmaz@example.com", Adres = "İstanbul, Türkiye" },
                new Hoca { Id = 2, AdSoyad = "Ayşe Demir", Telefon = "0987654321", Email = "ayse.demir@example.com", Adres = "Ankara, Türkiye" },
                new Hoca { Id = 3, AdSoyad = "Mehmet Can", Telefon = "5551234567", Email = "mehmet.can@example.com", Adres = "İzmir, Türkiye" },
                new Hoca { Id = 4, AdSoyad = "Fatma Korkmaz", Telefon = "4445678901", Email = "fatma.korkmaz@example.com", Adres = "Bursa, Türkiye" }
            );

            modelBuilder.Entity<Asistan>().HasData(
                new Asistan { Id = 1, AdSoyad = "Ali Yılmaz", Telefon = "1234567890", Email = "ali.yilmaz@example.com", Adres = "İstanbul, Türkiye" },
                new Asistan { Id = 2, AdSoyad = "Ayşe Demir", Telefon = "0987654321", Email = "ayse.demir@example.com", Adres = "Ankara, Türkiye" },
                new Asistan { Id = 3, AdSoyad = "Mehmet Can", Telefon = "5551234567", Email = "mehmet.can@example.com", Adres = "İzmir, Türkiye" },
                new Asistan { Id = 4, AdSoyad = "Fatma Korkmaz", Telefon = "4445678901", Email = "fatma.korkmaz@example.com", Adres = "Bursa, Türkiye" }
            );
        }
    }
}
