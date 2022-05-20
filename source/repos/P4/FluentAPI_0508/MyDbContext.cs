using Microsoft.EntityFrameworkCore;

namespace FluentAPI_0508;

internal class MyDbContext : DbContext 
{ 
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JJ2EHR8\SQLEXPRESS;Initial Catalog=PIV2-Relations;Integrated Security=True;TrustServerCertificate=True;");
    }
    
//Metoda fluent
    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Client>().Property("Id")
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("Nvarchar");
    }*/
}