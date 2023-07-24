using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MagazineApp1.Models
{
    public partial class INTERNContext : DbContext
    {
        public INTERNContext()
        {
        }

        public INTERNContext(DbContextOptions<INTERNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MagazineTable> MagazineTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=dindapptr126-31.ptg.local;Database=INTERN;User Id=usr_intern;Password=%i1V@2E97Q49;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MagazineTable>(entity =>
            {
                entity.HasKey(e => e.Magid);

                entity.ToTable("magazine_table");

                entity.Property(e => e.Magid).HasColumnName("magid");

                entity.Property(e => e.Magdate)
                    .HasColumnType("date")
                    .HasColumnName("magdate");

                entity.Property(e => e.Magdesc)
                    .HasMaxLength(200)
                    .HasColumnName("magdesc");

                entity.Property(e => e.Magimg)
                    .HasMaxLength(50)
                    .HasColumnName("magimg");

                entity.Property(e => e.Magtitle)
                    .HasMaxLength(100)
                    .HasColumnName("magtitle");

                entity.Property(e => e.Magurl)
                    .HasMaxLength(250)
                    .HasColumnName("magurl");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
