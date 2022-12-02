using Microsoft.EntityFrameworkCore;

namespace LunchSessionBlazor.Api.Persistence
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {

        }

        public DbSet<TodoEntity> TodoItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoEntity>()
                .HasKey(x => x.Id);
        }
    }
}
