using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dataservice.ExpModels
{
    public partial class _17SP2G4Context : DbContext
    {
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source = dt-srv-web4.ehb.local; Initial Catalog = 17SP2G4; Persist Security Info = True; User ID = 17SP2G4; Password = vj13dnpy25;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.HasIndex(e => e.EmpId)
                    .HasName("idx_empid_notnull")
                    .IsUnique()
                    .HasFilter("([empid] IS NOT NULL)");

                entity.HasIndex(e => e.Username)
                    .HasName("UK_USER_USERNAME")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320);

                entity.Property(e => e.EmpId).HasColumnName("empID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("nchar(40)");
            });
        }
    }
}
