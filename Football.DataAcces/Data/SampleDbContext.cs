using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Basketball.Entity.Models;  // Kendi Entity modellerinizi eklediğiniz namespace

namespace Football.DataAcces.Data
{
    // IdentityDbContext ile User ve Role sınıflarını belirtiyoruz
    public class SampleDBContext : IdentityDbContext<User, Role, int>
    {
        public SampleDBContext(DbContextOptions<SampleDBContext> options)
            : base(options)
        {
        }

        public SampleDBContext()
        {
        }

        // DbSet tanımlamalarınız burada
        public virtual DbSet<CategoryGroups> CategoryGroups { get; set; }
        public virtual DbSet<Dues> Dues { get; set; }
        public virtual DbSet<TrainingHours> TrainingHours { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }

        // OnConfiguring metodunu değiştirmeyi unutmayın. Aşağıdaki gibi bir bağlantı dizesi ile bağlanabilirsiniz.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=DL-MAKTAS1;Initial Catalog=Academy;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
                .EnableSensitiveDataLogging();
        }

        // İhtiyacınıza göre özelleştirebileceğiniz OnModelCreating metodunu kullanabilirsiniz
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Burada identity ile ilgili özelleştirmeler yapabilirsiniz (örneğin, User veya Role üzerinde)
        }
    }
}
