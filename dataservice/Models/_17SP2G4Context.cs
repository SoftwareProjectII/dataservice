using Microsoft.EntityFrameworkCore;

namespace dataservice.Models
{
    public partial class _17SP2G4Context : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Traininginfo> Traininginfo { get; set; }

        public _17SP2G4Context(DbContextOptions<_17SP2G4Context> options) : base(options)
        { } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("ADDRESS");

                entity.Property(e => e.AddressId).HasColumnName("addressID");

                entity.Property(e => e.AdministrativeArea)
                    .IsRequired()
                    .HasColumnName("administrativeArea")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.Locality)
                    .IsRequired()
                    .HasColumnName("locality")
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasMaxLength(20);

                entity.Property(e => e.Premise)
                    .HasColumnName("premise")
                    .HasMaxLength(30);

                entity.Property(e => e.StreetAddress)
                    .IsRequired()
                    .HasColumnName("streetAddress")
                    .HasMaxLength(85);
            });

            modelBuilder.Entity<Traininginfo>(entity =>
            {
                entity.HasKey(e => e.TrainingId);

                entity.ToTable("TRAININGINFO");

                entity.Property(e => e.TrainingId).HasColumnName("trainingID");

                entity.Property(e => e.InfoExam)
                    .IsRequired()
                    .HasColumnName("infoExam");

                entity.Property(e => e.InfoGeneral)
                    .IsRequired()
                    .HasColumnName("infoGeneral");

                entity.Property(e => e.InfoPayment)
                    .IsRequired()
                    .HasColumnName("infoPayment");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.NumberOfDays).HasColumnName("numberOfDays");

                entity.Property(e => e.Price).HasColumnName("price");
            });
        }
    }
}
