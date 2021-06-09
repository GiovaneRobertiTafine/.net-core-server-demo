using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql;
using CadastroSerie.DataAccess.DataObjects;

#nullable disable

namespace CadastroSerie.DataAccess.DataObjects
{
    public partial class WorldContext : DbContext
    {
        
        public WorldContext()
        {
            
        }
        
        public WorldContext(DbContextOptions<WorldContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Countrylanguage> Countrylanguages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
            //.SetBasePath("path here") //<--You would need to set the path
            .AddJsonFile("appsettings.json"); //or what ever file you have the settings

            IConfiguration configuration = builder.Build();

            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.CountryCode, "CountryCode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("city_ibfk_1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PRIMARY");

                entity.ToTable("country");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.Code2)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.Continent)
                    .IsRequired()
                    .HasColumnType("enum('Asia','Europe','North America','Africa','Oceania','Antarctica','South America')")
                    .HasDefaultValueSql("'Asia'");

                entity.Property(e => e.Gnp)
                    .HasColumnType("float(10,2)")
                    .HasColumnName("GNP");

                entity.Property(e => e.Gnpold)
                    .HasColumnType("float(10,2)")
                    .HasColumnName("GNPOld");

                entity.Property(e => e.GovernmentForm)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.HeadOfState)
                    .HasMaxLength(60)
                    .IsFixedLength(true);

                entity.Property(e => e.LifeExpectancy).HasColumnType("float(3,1)");

                entity.Property(e => e.LocalName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(52)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(26)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.SurfaceArea).HasColumnType("float(10,2)");
            });

            modelBuilder.Entity<Countrylanguage>(entity =>
            {
                entity.HasKey(e => new { e.CountryCode, e.Language })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("countrylanguage");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.HasIndex(e => e.CountryCode, "CountryCode");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(3)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.Language)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("''")
                    .IsFixedLength(true);

                entity.Property(e => e.IsOfficial)
                    .IsRequired()
                    .HasColumnType("enum('T','F')")
                    .HasDefaultValueSql("'F'");

                entity.Property(e => e.Percentage).HasColumnType("float(4,1)");

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Countrylanguages)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("countryLanguage_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
