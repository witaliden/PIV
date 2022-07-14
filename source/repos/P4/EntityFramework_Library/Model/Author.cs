using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_Library.Model
{
    [Table("Authors")]
    public class Author
    {
        public Author()
        {
            AuthorBooks = new HashSet<Book>();
        }
        public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<Book>? AuthorBooks { get; set; }
        
        public override string ToString()
        {
            return "book " + AuthorId + " " + FirstName + " " + LastName;
        }
    }
}