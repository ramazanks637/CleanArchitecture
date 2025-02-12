using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistance.Configurations
{
    public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars"); // bu entity'nin veritabanında hangi tabloya karşılık geleceğini belirtir.
            builder.HasKey(x => x.Id); // bu entity'nin primary key'i hangi property'e sahip olacak onu belirtir.
        }
    }
}
