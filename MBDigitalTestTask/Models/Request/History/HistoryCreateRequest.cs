using System.ComponentModel.DataAnnotations;

namespace MBDigitalTestTask.Models.Request.History
{
    public class HistoryCreateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime BorrowedDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
