using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Project_1.Models;

namespace Project_1.DB
{
    public partial class MyDbXontext : DbContext
    {
        public MyDbXontext()
        {
        }

        public MyDbXontext(DbContextOptions<MyDbXontext> options)
            : base(options)
        {
        }
        public virtual DbSet<Pracownicy> PracownicyDbSet { get; set; } = null!;
        public virtual DbSet<Przejazdy> PrzejazdyDbSet { get; set; } = null!;
        public virtual DbSet<Samochody> SamochodyDbSet { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Ewidencja samochodow;Trusted_Connection=True;");
            }
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pracownicy>(entity =>
            {
                entity.HasKey(e => e.PracownikId)
                    .HasName("PK__Pracowni__6848293ADD25019E");

                entity.ToTable("Pracownicy");

                entity.Property(e => e.PracownikId).HasColumnName("PracownikID");

                entity.Property(e => e.Imie)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Pesel)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PESEL")
                    .IsFixedLength();

                entity.Property(e => e.Plec)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Stanowisko)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Przejazdy>(entity =>
            {
                entity.HasKey(e => e.PrzejazdId)
                    .HasName("PK__Przejazd__04BBB10379A9FB83");

                entity.ToTable("Przejazdy");

                entity.Property(e => e.PrzejazdId).HasColumnName("PrzejazdID");

                entity.Property(e => e.CelWyjazdu)
                    .HasMaxLength(90)
                    .IsUnicode(false);

                entity.Property(e => e.DataCzasOdbioru).HasColumnType("datetime");

                entity.Property(e => e.DataCzasZwrotu).HasColumnType("datetime");

                entity.Property(e => e.PracownikId).HasColumnName("PracownikID");

                entity.Property(e => e.Vin)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("VIN")
                    .IsFixedLength();

                entity.HasOne(d => d.Pracownik)
                    .WithMany(p => p.PracownikPrzejazd)
                    .HasForeignKey(d => d.PracownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PracownikID");

                entity.HasOne(d => d.VinNavigation)
                    .WithMany(p => p.samochodPrzejazd)
                    .HasForeignKey(d => d.Vin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_VIN");
            });

            modelBuilder.Entity<Samochody>(entity =>
            {
                entity.HasKey(e => e.Vin)
                    .HasName("PK__Samochod__C5DF234DC486B542");

                entity.ToTable("Samochody");

                entity.Property(e => e.Vin)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("VIN")
                    .IsFixedLength();

                entity.Property(e => e.Dostepnosc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Marka)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Model)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NumerRejestracyjny)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
