using System.ComponentModel.DataAnnotations;

namespace MBDigitalTestTask.Models.Request.Book
{
    public class UpdateBookRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int LibraryId { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public int TotalPages { get; set; }
    }
}
