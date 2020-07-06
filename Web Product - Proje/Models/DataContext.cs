namespace WebProduct.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Ceo> Ceos { get; set; }
        public virtual DbSet<FirmaBilgi> FirmaBilgis { get; set; }
        public virtual DbSet<Hesap> Hesaps { get; set; }
        public virtual DbSet<HomeSlider> HomeSliders { get; set; }
        public virtual DbSet<Kategoriler> Kategorilers { get; set; }
        public virtual DbSet<Mesajlar> Mesajlars { get; set; }
        public virtual DbSet<Popup> Popups { get; set; }
        public virtual DbSet<Sabitler> Sabitlers { get; set; }
        public virtual DbSet<Sayfalar> Sayfalars { get; set; }
        public virtual DbSet<SosyalMedya> SosyalMedyas { get; set; }
        public virtual DbSet<Urunler> Urunlers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ceo>()
                .Property(e => e.SiteKurulus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Kategoriler>()
                .HasMany(e => e.Urunlers)
                .WithOptional(e => e.Kategoriler)
                .HasForeignKey(e => e.KategoriNo);

            modelBuilder.Entity<Sabitler>()
                .Property(e => e.HomeResBaslik)
                .IsFixedLength();
        }
    }
}
