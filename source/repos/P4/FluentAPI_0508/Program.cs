using FluentAPI_0508;
using Microsoft.EntityFrameworkCore;

var context = new MyDbContext();
context.Database.EnsureCreated();

/*var client = new Client()
{
    Name = "Jan Nowak"
};

client.Orders.Add(new Order()
{
    Price = 10m
});

context.Clients.Add(client);
context.SaveChanges();*/

foreach (var item in context.Clients.Include(x => x.Orders).ToList())
{
    Console.WriteLine(item.Name);
}