using System;
using Detector.Core.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Detector.Infrastructure.Database
{
    public partial class DataContext : DbContext//, IDataContext
    {
        private readonly SqlSettings _settings;

        public DataContext(DbContextOptions<DataContext> options, SqlSettings settings)
            : base(options)
        {
            _settings = settings;
        }

        public virtual DbSet<ImageDb> Image { get; set; }
        public virtual DbSet<StatisticsDb> Statistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                System.Console.WriteLine(_settings.ConnectionString);
                optionsBuilder.UseMySql(_settings.ConnectionString, x => x.ServerVersion("5.7.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageDb>(entity =>
            {
                entity.ToTable("image");

                entity.HasIndex(e => e.Id)
                    .HasName("Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StatsId)
                    .HasName("StatsId_idx");

                entity.Property(e => e.Id)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.Added).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.PublicId)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.Property(e => e.StatsId).HasColumnType("int(11)");

                entity.Property(e => e.Url)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_bin");

                entity.HasOne(d => d.Stats)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.StatsId)
                    .HasConstraintName("StatsId");
            });

            modelBuilder.Entity<StatisticsDb>(entity =>
            {
                entity.ToTable("statistics");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Stat1)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Stat2)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Stat3)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
