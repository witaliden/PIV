using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_Library
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public IQueryable<Book> AuthorBooks { get; set; }
        [InverseProperty(nameof(Book.Author))]
        public virtual ICollection<Book>? AuthorBooks { get; set; }
        //public virtual List<Book>? Books { get; set; }
        
    }
}