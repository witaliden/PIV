using EntityFramework_Library.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_Library.db;

public partial class MyDbContext : DbContext
{
    public MyDbContext() { }
    public MyDbContext(DbContextOptions<MyDbContext> options) :base(options) { }

    public DbSet<Author>? Authors { get; set; }
    public DbSet<Book>? Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            @"Data Source=DESKTOP-JJ2EHR8\SQLEXPRESS;Initial Catalog=PIV-Library;Integrated Security=True;TrustServerCertificate=True;"); 
        //Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasOne(d => d.Author)
                .WithMany(p => p.AuthorBooks)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fK_authorID");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}