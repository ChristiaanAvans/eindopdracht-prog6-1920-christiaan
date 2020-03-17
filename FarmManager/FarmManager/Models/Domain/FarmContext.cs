namespace FarmManager.Models.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FarmContext : DbContext
    {
        public FarmContext()
            : base("name=FarmContext")
        {
        }

        public virtual DbSet<Accessoire> Accessoires { get; set; }
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessoire>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Accessoire>()
                .Property(e => e.Price)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Accessoire>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Accessoire>()
                .HasMany(e => e.Bookings)
                .WithMany(e => e.Accessoires)
                .Map(m => m.ToTable("BookedAccessoire").MapLeftKey("Accessoire").MapRightKey("Booking"));

            modelBuilder.Entity<Animal>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Animal>()
                .Property(e => e.Price)
                .HasPrecision(20, 2);

            modelBuilder.Entity<Animal>()
                .Property(e => e.ImageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Animal>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<Animal>()
                .HasMany(e => e.Accessoires)
                .WithRequired(e => e.Animal1)
                .HasForeignKey(e => e.Animal);

            modelBuilder.Entity<Animal>()
                .HasMany(e => e.Bookings)
                .WithMany(e => e.Animals)
                .Map(m => m.ToTable("BookedAnimal").MapLeftKey("Animal").MapRightKey("Booking"));

            modelBuilder.Entity<Booking>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.Insertion)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
