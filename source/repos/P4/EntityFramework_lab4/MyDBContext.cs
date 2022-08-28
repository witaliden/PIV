using Microsoft.EntityFrameworkCore;
using System;
namespace EntityFramework_lab4;

    internal class MyDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Client> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JJ2EHR8\SQLEXPRESS;Initial Catalog=Northwind1;Integrated Security=True");
            //optionsBuilder.UseSqlServer(@"jdbc:jtds:sqlserver://./");
            
        } 
    }