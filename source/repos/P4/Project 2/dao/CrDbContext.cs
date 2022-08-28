using Microsoft.EntityFrameworkCore;
using Project_2.model;

namespace Project_2.dao
{
    internal class CrDbContext : DbContext
    {
        public virtual DbSet<Employee>? EmployeeDbSet { get; set; }
        public virtual DbSet<DriverLicense>? DriverLicenseDbSet { get; set; }
        public virtual DbSet<CarOwner>? CarOwnersDbSet { get; set; }
        public virtual DbSet<Car> CarDbSet { get; set; } = null!;
        public virtual DbSet<Trip> TripDbSet { get; set; } = null!;

        /*public CrDbContext(DbContextOptions<CrDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JJ2EHR8\SQLEXPRESS;Initial Catalog=Project2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
