using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dataservice.Models
{
    public partial class _17SP2G4Context : DbContext
    {

        public _17SP2G4Context(DbContextOptions<_17SP2G4Context> options)
            :base(options)
        {

        }
        public virtual DbSet<Test> Test { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("test");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasColumnType("ntext");
            });
        }
    }
}
