using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Natech.IpGeoFinder.DAL.DataTypes;

#nullable disable

namespace Natech.IpGeoFinder.DAL
{
    public partial class GeoIpDBContext : DbContext
    {
        public GeoIpDBContext()
        {
        }

        public GeoIpDBContext(DbContextOptions<GeoIpDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<BatchDetail> BatchDetails { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source = localhost; Initial Catalog = GeoIpDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batch");

                entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Batch_Status");
            });

            modelBuilder.Entity<BatchDetail>(entity =>
            {
                entity.HasIndex(e => new { e.Id, e.BatchId, e.FetchedDateTime }, "NonClusteredIndex-20210130-000253");

                entity.Property(e => e.CountryCode).HasMaxLength(10);

                entity.Property(e => e.CountryName).HasMaxLength(70);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 9)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 9)");

                entity.Property(e => e.TimeZone).HasMaxLength(70);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchDetails)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchDetails_Batch");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Literal)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
