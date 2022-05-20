using Microsoft.EntityFrameworkCore;
using EntityFramework_lab4;

var context = new MyDbContext();
context.Database.EnsureCreated();
context.Database.Migrate();

using var transactions = context.Database.BeginTransaction();

context.Clients.Add(new Client() {
    Name = "Jan Kowalski 2",
    Address = "Szeroka, Bielsko-Biała",
    Balance = 0
});

//transactions.Rollback();
//transactions.Commit();

context.SaveChanges();

var result = context.Clients.Where(client => client.Balance == 0).ToArray();
//var result = context.Products.Where(client => client.Price > 0).ToArray();

foreach (var item in result)
{
    Console.WriteLine(item.Name);
}