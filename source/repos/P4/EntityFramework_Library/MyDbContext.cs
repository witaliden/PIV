using Microsoft.EntityFrameworkCore;
using System;
namespace EntityFramework_Library;

internal class MyDbContext : DbContext
{
    public DbSet<Author>? Authors { get; set; }
    public DbSet<Book>? Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            @"Data Source=DESKTOP-JJ2EHR8\SQLEXPRESS;Initial Catalog=PIV2-Library;Integrated Security=True;TrustServerCertificate=True;");
    }
}