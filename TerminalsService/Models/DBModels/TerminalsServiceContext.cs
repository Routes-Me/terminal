using Microsoft.EntityFrameworkCore;

namespace TerminalsService.Models.DBModels
{
    public partial class TerminalsServiceContext : DbContext
    {
        public TerminalsServiceContext()
        {
        }

        public TerminalsServiceContext(DbContextOptions<TerminalsServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Terminals> Terminals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Terminals>(entity =>
            {
                entity.HasKey(e => e.TerminalId).HasName("PRIMARY");

                entity.ToTable("terminals");

                entity.Property(e => e.TerminalId).HasColumnName("terminal_id");

                entity.Property(e => e.NotificationIdentifier)
                    .HasColumnName("notification_identifier")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
