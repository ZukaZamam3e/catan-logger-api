using Microsoft.EntityFrameworkCore;
using CatanLoggerData.Entities;

namespace CatanLoggerData.Context;

public class CatanLoggerDbContext : DbContext
{
    public CatanLoggerDbContext(DbContextOptions<CatanLoggerDbContext> options)
        : base(options)
    {
    }

    public DbSet<CL_GAME> CL_GAME { get; set; }
    public DbSet<CL_PLAYER> CL_PLAYER { get; set; }
    public DbSet<CL_DICEROLL> CL_DICEROLL { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CL_GAME>().HasKey(m => m.GAME_ID);
        modelBuilder.Entity<CL_PLAYER>().HasKey(m => m.PLAYER_ID);
        modelBuilder.Entity<CL_DICEROLL>().HasKey(m => m.DICE_ROLL_ID);

        modelBuilder.Entity<CL_GAME>().HasMany(m => m.PLAYERS)
            .WithOne(m => m.GAME)
            .HasForeignKey(m => m.GAME_ID);

        modelBuilder.Entity<CL_GAME>().HasMany(m => m.DICE_ROLLS)
            .WithOne(m => m.GAME)
            .HasForeignKey(m => m.GAME_ID);

        modelBuilder.Entity<CL_GAME>(entity =>
        {
            entity.Property(e => e.GAME_ID)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.USER_ID)
                .IsRequired();

            entity.Property(e => e.DATE_PLAYED)
                .IsRequired();

        });

        modelBuilder.Entity<CL_PLAYER>(entity =>
        {
            entity.Property(e => e.PLAYER_ID)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.GAME_ID)
                .IsRequired();

            entity.Property(e => e.PLAYER_NAME)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PLAYER_COLOR)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.TURN_ORDER)
                .IsRequired();

            entity.Property(e => e.WINNER)
                .IsRequired();
        });

        modelBuilder.Entity<CL_DICEROLL>(entity =>
        {
            entity.Property(e => e.DICE_ROLL_ID)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DICE_NUMBER)
                .IsRequired();

            entity.Property(e => e.DICE_ROLLS)
                .IsRequired();
        });
    }
}
