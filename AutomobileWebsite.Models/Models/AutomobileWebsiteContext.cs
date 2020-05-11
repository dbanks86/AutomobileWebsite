using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutomobileWebsite.Models.Models
{
    public partial class AutomobileWebsiteContext : DbContext
    {
        public AutomobileWebsiteContext()
        {
        }

        public AutomobileWebsiteContext(DbContextOptions<AutomobileWebsiteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Dealership> Dealerships { get; set; }
        public virtual DbSet<DealershipAddress> DealershipAddresses { get; set; }
        public virtual DbSet<State> States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER01;Database=AutomobileWebsite;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasIndex(e => e.Make);

                entity.HasIndex(e => e.Model);

                entity.Property(e => e.CarId)
                .HasColumnName("CarID")
                .UseIdentityColumn();

                entity.Property(e => e.DealershipAddressId).HasColumnName("DealershipAddressID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(19, 2)");

                entity.Property(e => e.Trim)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.DealershipAddress)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.DealershipAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cars_DealershipAddresses");
            });

            modelBuilder.Entity<Dealership>(entity =>
            {
                entity.HasIndex(e => e.DealershipName)
                    .HasName("UQ_DealershipName");

                entity.HasIndex(e => e.WebsiteUrl)
                    .HasName("UQ_Website")
                    .IsUnique();

                entity.Property(e => e.DealershipId)
                .HasColumnName("DealershipID")
                .UseIdentityColumn();

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DealershipName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.WebsiteUrl)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DealershipAddress>(entity =>
            {
                entity.HasIndex(e => e.City);

                entity.HasIndex(e => e.Street)
                    .HasName("UQ_Street")
                    .IsUnique();

                entity.HasIndex(e => e.ZipCode);

                entity.Property(e => e.DealershipAddressId)
                .HasColumnName("DealershipAddressID")
                .UseIdentityColumn();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.DealershipId).HasColumnName("DealershipID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dealership)
                    .WithMany(p => p.DealershipAddresses)
                    .HasForeignKey(d => d.DealershipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DealershipAddresses_Dealerships");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.DealershipAddresses)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DealershipAddresses_States");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasIndex(e => e.StateAbbreviation)
                    .HasName("UQ_StateAbbreviation");

                entity.HasIndex(e => e.StateName)
                    .HasName("UQ_StateName");

                entity.Property(e => e.StateId)
                .HasColumnName("StateID")
                .UseIdentityColumn();

                entity.Property(e => e.StateAbbreviation)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
