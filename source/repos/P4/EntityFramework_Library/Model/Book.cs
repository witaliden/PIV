namespace EntityFramework_Library.Model
{
    public class Book
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; } = null;
        
        public override string ToString()
        {
            return $"Książka: {BookId} {Title} {Year}";
        }
    }
}
