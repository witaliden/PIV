using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_Library
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
        
        [ForeignKey(nameof(AuthorId))]
        //[InverseProperty("Books")]
        public virtual Author? Author { get; }
        //public IQueryable<Author> BookAuthors { get; set; }
    
    }
}
