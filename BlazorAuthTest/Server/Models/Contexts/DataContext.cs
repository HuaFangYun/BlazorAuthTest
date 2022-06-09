using BlazorAuthTest.Server.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlazorAuthTest.Server.Models.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().HasKey(x => x.Id);
        }

        public virtual DbSet<AppUser> Users { get; set; }
    }


    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite("Data Source=../Server/authtest.db")
                .EnableSensitiveDataLogging();

            return new DataContext(optionsBuilder.Options);
        }
    }
}
