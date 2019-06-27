using DataAccess.Ef.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Ef
{
    public partial class ProSoftDbContext : DbContext
    {
        public ProSoftDbContext()
        {
        }

        public ProSoftDbContext(DbContextOptions<ProSoftDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartment> Apartment { get; set; }
        public virtual DbSet<Indication> Indication { get; set; }
        public virtual DbSet<Meter> Meter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=prosoftdb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("apartment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Meterid).HasColumnName("meterid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Meter)
                    .WithMany(p => p.Apartment)
                    .HasForeignKey(d => d.Meterid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_apertment_meter_meterid");
            });

            modelBuilder.Entity<Indication>(entity =>
            {
                entity.ToTable("indication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datevalue)
                    .HasColumnName("datevalue")
                    .HasColumnType("date");

                entity.Property(e => e.Meterid).HasColumnName("meterid");

                entity.Property(e => e.Value).HasColumnName("value");

                //entity.HasOne(d => d.Meter)
                //    .WithMany(p => p.Indication)
                //    .HasForeignKey(d => d.Meterid);
            });

            modelBuilder.Entity<Meter>(entity =>
            {
                entity.ToTable("meter");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lastverification)
                    .HasColumnName("lastverification")
                    .HasColumnType("datetime");

                entity.Property(e => e.Nextverification)
                    .HasColumnName("nextverification")
                    .HasColumnType("datetime");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(255);
            });
        }
    }
}
