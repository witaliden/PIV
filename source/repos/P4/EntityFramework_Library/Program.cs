using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using EntityFramework_Library;
using Microsoft.EntityFrameworkCore.ChangeTracking;

var context = new MyDbContext();
context.Database.EnsureCreated();
//context.Entry(Author).State = EntityState.Detached;

//Wpisuje do bazy losowe dane
if (context.Authors.Count() < 1)
{
    FillAuthors();
    Thread.Sleep(3000);
    FillBooks();
    Thread.Sleep(4000);
}

Console.WriteLine("Witamy w bibliotece \n");
Console.WriteLine("Wpisz 1 by wyszukać autora");
Console.WriteLine("Wpisz 2 by wyszukać książki");
Console.WriteLine("Wpisz 3 by zamknąć aplikację");

short userChoise;
do {
    userChoise = Convert.ToInt16(Console.ReadLine());
    switch (userChoise) {
        case 1: 
            GetAuthor(); 
            Console.WriteLine("\n"); 
            break; 
        case 2: 
            GetBook(); 
            Console.WriteLine("\n"); 
            break; 
    } 
} while (userChoise != 3);



//wypełnienie tabeli autorów
void FillAuthors() 
{ 
    for (var i = 1; i <= 10; i++) 
    {
        context.Authors!.Add((new Author() 
        { 
            FirstName = Randomizer.RandomString(RandomNumberGenerator.GetInt32(30)), 
            LastName = Randomizer.RandomString(i + 7) // DML(
        }));
    } context?.SaveChanges();
}

//wypełnienie tabeli książek
void FillBooks() 
{
    var tempAuthorList = GetAllAuthors(); 
    foreach (var a in tempAuthorList) 
    { 
        for (var i = 1; i <= 10; i++) 
        {
            EntityEntry<Book> b = context!.Books!.Add(new Book() 
            { 
                Title = Randomizer.RandomString(i + 7), 
                Year = 2022 + i, 
                AuthorId = a
            });
        }
    } context?.SaveChanges();
}

void GetBook() { 
    Console.WriteLine("Podaj tytuł książki: ");
    
    var keyWord = Console.ReadLine()!.Trim(); 
    if(context.Authors is null) return; 
    var selectedBooks = context.Books!
        .Include(b => b.Author)!
        .Where(b => b.Title.Contains(keyWord!))
        .ToList();

    foreach (var book in selectedBooks) {
        Console.WriteLine("\n" + Environment.NewLine); 
        Console.WriteLine($"Tytuł: {book.Title}"); 
        Console.WriteLine($"Autor: {book.Author!.FirstName} {book.Author.LastName}"); 
        Console.WriteLine($"Rok wydania: {book.Year}"); 
    }
}

void GetAuthor() { 
    Console.WriteLine("Podaj nazwisko autora: \n"); 
    var keyWord = Console.ReadLine()!.Trim(); 
    if (context.Authors == null) return;
    var authorBooks = context.Authors.Include(ab => ab.AuthorBooks)
        .ThenInclude(book => book.Author).Where(a => a.LastName.Equals(keyWord))
        .SelectMany(a => a.AuthorBooks).ToList();
    //authorBooks.FirstOrDefault().Books.Add();

    Console.WriteLine("Książki autora: ");
    foreach (var book in authorBooks) {
        Console.WriteLine("\n" + Environment.NewLine); 
        Console.WriteLine($"Tytuł: {book.Title}"); 
        Console.WriteLine($"Autor: {book.Author!.FirstName} {book.Author.LastName}"); 
        Console.WriteLine($"Rok wydania: {book.Year}"); 
    }
}

List<int> GetAllAuthors()
{
    if (context!.Authors == null) return null;
    List<int> authors = context.Authors
        .Select(a => a.AuthorId).ToList();
    return authors;
}