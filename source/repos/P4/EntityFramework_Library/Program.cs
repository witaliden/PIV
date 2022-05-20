using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using EntityFramework_Library;
using Microsoft.VisualBasic;

var context = new MyDbContext();
context.Database.EnsureCreated();
context.Database.EnsureCreated();
context.Database.Migrate();

//using var transactions = context.Database.BeginTransaction();

for (var i = 1; i <= 20; i++)
{
    context.Authors!.Add(new Author() {
        AuthorId = i,
        FirstName = Randomizer.RandomString(RandomNumberGenerator.GetInt32(30)),
        LastName = Randomizer.RandomString(i + 7)
    });
    context.Books!.Add(new() {
        BookId = i,
        Title = Randomizer.RandomString(i + 7),
        Year = 2022 + i,
        //Author = context.Authors.TakeLast(aut)
    });
}

/*if (context.Authors != null)
{
    var result = context.Authors.ToArray();
}*/

context.SaveChanges();

/*void SearchLibrary()
{
    Console.WriteLine("Chcesz wyszukać książkę - podaj 1, autora - podaj 2.");
    var choice = Convert.ToInt32(Console.ReadLine());
    string userInput;
    switch (choice)
    {
        case 1:
            Console.WriteLine("Szukasz książkę. Podaj tytuł: ");
            userInput = Console.ReadLine()!.ToString().Trim();
            Console.WriteLine(context.Books!.Where(book => book.Title == userInput));
            break;
        case 2:
            Console.WriteLine("Szukasz autora. Podaj nazwisko: ");
            userInput = Console.ReadLine()!.ToString().Trim();
            Console.WriteLine(context.Authors!.Where(author => author.LastName == userInput));
                          //"\n" + context.Books!.Where(book => book.AuthorID == ));
            break;

    }
}*/
