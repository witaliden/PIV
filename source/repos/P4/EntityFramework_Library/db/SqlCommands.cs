using System.Security.Cryptography;
using EntityFramework_Library.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_Library.db
{
    public class SqlCommands
    {
        // wyświetlamy informację o książce
        public void GetBook(MyDbContext context, String keyWord)
        {
            if (context.Authors is null) return;
            var selectedBooks = context.Books!
                .Include(b => b.Author)!
                .Where(w => w.Title.Contains(keyWord!))
                .ToList();

            foreach (var book in selectedBooks)
            {
                Console.WriteLine("\n" + Environment.NewLine);
                Console.WriteLine($"Tytuł: {book.Title}");
                Console.WriteLine($"Autor: {book.Author!.FirstName} {book.Author.LastName}");
                Console.WriteLine($"Rok wydania: {book.Year}");
            }
        }

        // wyświetlamy autora i jego książki
        public void GetAuthor(MyDbContext context, String keyWord)
        {
            if (context.Authors == null) return;
            var authorBooks = context.Authors.Include(ab => ab.AuthorBooks)!
                .ThenInclude(book => book.Author).Where(a => a.LastName!.Equals(keyWord))
                .SelectMany(a => a.AuthorBooks!).ToList();

            Console.WriteLine("Książki autora: ");
            foreach (var book in authorBooks)
            {
                Console.WriteLine("\n" + Environment.NewLine);
                Console.WriteLine($"Tytuł: {book.Title}");
                Console.WriteLine($"Autor: {book.Author!.FirstName} {book.Author.LastName}");
                Console.WriteLine($"Rok wydania: {book.Year}");
            }
        }

        // pobieramy listę ID wszystkich autorów
        List<int> GetAllAuthorsId(MyDbContext context)
        {
            if (context!.Authors == null) return null!;
            List<int> authorsId = context.Authors
                .Select(a => a.AuthorId).ToList();
            return authorsId;
        }

        
        /*---------------Fill database---------------/
        /-------------------------------------------*/
        //wypełnienie tabeli autorów
        public void FillAuthors(MyDbContext context)
        {
            for (var i = 1; i <= 10; i++)
            {
                context.Authors!.Add((new Author()
                {
                    FirstName = Randomizer.RandomString(RandomNumberGenerator.GetInt32(30)),
                    LastName = Randomizer.RandomString(i + 7) //
                    
                }));
            }
            context?.SaveChanges();
        }

        //wypełnienie tabeli książek
        public void FillBooks(MyDbContext context)
        {
            var tempAuthorList = GetAllAuthorsId(context);
            foreach (var a in tempAuthorList)
            {
                for (var i = 1; i <= 10; i++)
                {
                    context!.Books!.Add(new Book()
                    {
                        Title = Randomizer.RandomString(i + 7),
                        Year = 2022 + i,
                        AuthorId = a
                    });
                }
            }
            context?.SaveChanges();
        }
    }
}