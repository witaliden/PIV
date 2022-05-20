using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_Library;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int AuthorId { get; set; }
    //public Author? Author { get; set; }
    //public IQueryable<Book> Author { get; set; }
}