using System.Reflection;
using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Context
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReferance).Assembly);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreatedDate).CurrentValue = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(p => p.UpdateDate).CurrentValue = DateTime.UtcNow;
                }


            }


            return base.SaveChangesAsync(cancellationToken);
        }
















        //AppDbContext context = new();
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Database=CleanArchitecture;Username=postgres;Password=123456");
        //}

        // aşağıdaki yöntem sayesinde connection string'i appsettings.json dosyasından alabiliriz.
        // best practice olarak bu yöntemi kullanmamız önerilir.


    }
}
