using Microsoft.EntityFrameworkCore;

namespace RecipeLib.Entities
{
    public class AppDbContext : DbContext
    {

        private string _connectionString = "Data Source= localhost;User Id=recipeapp;password=zaq1@WSX;Trusted_Connection=True;MultipleActiveResultSets=true; Integrated Security = false; DATABASE=recipedb";
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Nickname)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}