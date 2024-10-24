using System.ComponentModel.DataAnnotations.Schema;

namespace MBDigitalTestTask.Models.Entities
{
    public class Book : Entity
    {          
        public string Title { get; set; }        
        public DateTime PublishedDate { get; set; } 
        public string ISBN { get; set; }
        public int TotalPages { get; set; }
        public string? Language { get; set; }
        public int AuthorId { get; set; }       
        public virtual Author Author { get; set; }
        public virtual ICollection<BookLibrary> BookLibraries { get; set; }
        public virtual ICollection<BorrowingHistory> History { get; set; }

        [NotMapped]
        public int LibraryId {  get; set; }
    }
}
